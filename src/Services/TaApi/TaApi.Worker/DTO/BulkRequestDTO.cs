using System.Text.Json.Serialization;
using TaApi.Worker.Data;

namespace TaApi.Worker.DTO
{
    public class BulkRequestDTO
    {
        public string secret { get; set; }

        public IEnumerable<BulkRequestConstructDTO> construct { get; set; }
    }

    public class BulkRequestConstructDTO
    {
        public string exchange { get; set; }

        public string symbol { get; set; }

        public string interval { get; set; }

        public IEnumerable<BulkRequestIndicatorDTO> indicators { get; set; }
    }

    public class BulkRequestIndicatorDTO
    {
        public string indicator { get; set; }

        public int period { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int backtracks { get; set; }

        public bool addResultTimestamp { get; set; }

        public BulkRequestIndicatorDTO()
        {
        }

        public BulkRequestIndicatorDTO(TAIndicator taIndicator, int period, int backtracks = default)
        {
            this.indicator = taIndicator.Value;
            this.period = period;
            this.backtracks = backtracks;
            this.addResultTimestamp = true;
        }
    }
}