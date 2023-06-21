﻿using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using TaApi.BackgroundTasks.Abstraction;
using TaApi.BackgroundTasks.Configuration;
using TaApi.BackgroundTasks.DTO;
using TaApi.BackgroundTasks.Entities;
using TaApi.BackgroundTasks.Settings;
using TaApi.BackgroundTasks.Data;

namespace TaApi.BackgroundTasks.Services
{
    public class TaApiClient : ITaApiClient
    {
        private readonly TaApiOptions _taApiOptions;

        public TaApiClient(IOptions<TaApiOptions> taApiOptions)
        {
            _taApiOptions = taApiOptions.Value;
        }

        public async Task<TEntity?> GetEntity<TEntity, TDTO>(string symbol, TAInterval taInterval, TAIndicator taIndicator, string extraParameters)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_taApiOptions.TaApiUrl)
            };

            var dto = await httpClient.GetFromJsonAsync<TDTO>($"{taIndicator.Value}?secret={_taApiOptions.TaApiKey}&exchange={ConstSettings.TA_API_EXCAHNGE}&symbol={symbol}&interval={taInterval.Value}&addResultTimestamp=true&{extraParameters}");

            return dto != null ? dto.GetEntity() : null;
        }

        public async Task<IEnumerable<TEntity>> GetEntities<TEntity, TDTO>(string symbol, TAInterval taInterval, TAIndicator taIndicator, string extraParameters)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_taApiOptions.TaApiUrl)
            };

            var requestUri = $"{taIndicator.Value}?secret={_taApiOptions.TaApiKey}&exchange={ConstSettings.TA_API_EXCAHNGE}&symbol={symbol}&interval={taInterval.Value}&addResultTimestamp=true&backtracks=20&{extraParameters}";

            var dtos = await httpClient.GetFromJsonAsync<IEnumerable<TDTO>>(requestUri);

            return dtos != null ? dtos.Select(v => v.GetEntity()) : new List<TEntity>();
        }

        public async Task<IEnumerable<Tuple<BulkResponseIdEntity, TEntity>>> GetBulkEntities<TEntity, TDTO>(IEnumerable<string> symbols, IEnumerable<BulkRequestIndicatorDTO> indicators, TAInterval taInterval)
            where TEntity : BaseEntity, new()
            where TDTO : BaseDTO<TEntity>
        {
            var resultList = new List<Tuple<BulkResponseIdEntity, TEntity>>();

            if (symbols == null || !symbols.Any())
                return resultList;

            if (indicators == null || !indicators.Any())
                return resultList;

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_taApiOptions.TaApiUrl)
            };

            var constructs = new List<BulkRequestConstructDTO>();
            foreach (var symbol in symbols)
            {
                var construct = new BulkRequestConstructDTO
                {
                    exchange = ConstSettings.TA_API_EXCAHNGE,
                    symbol = symbol,
                    interval = taInterval.Value,
                    indicators = indicators
                };

                constructs.Add(construct);
            }

            var bulkRequestDTO = new BulkRequestDTO
            {
                secret = _taApiOptions.TaApiKey,
                construct = constructs
            };

            using var bulkResponse = await httpClient.PostAsJsonAsync("bulk", bulkRequestDTO);
            if (bulkResponse == null || !bulkResponse.IsSuccessStatusCode)
            {
                var message = bulkResponse != null
                    ? await bulkResponse.Content.ReadAsStringAsync()
                    : "missing response";
                throw new Exception($"TaAPI Exception: {message}");
            }

            var responseDTO = await bulkResponse.Content.ReadFromJsonAsync<BulkResponseDTO<TEntity, TDTO>>();
            if (responseDTO != null && responseDTO.data != null)
            {
                foreach (var itemDTO in responseDTO.data)
                {
                    var bulkResponseId = itemDTO.TryParseId();
                    var entity = itemDTO.result?.GetEntity();

                    if (bulkResponseId != null && entity != null)
                        resultList.Add(new Tuple<BulkResponseIdEntity, TEntity>(bulkResponseId, entity));
                }
            }

            return resultList;
        }
    }
}