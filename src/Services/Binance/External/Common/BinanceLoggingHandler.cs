namespace Binance.Common
{
    using System.Net.Http;
    using System.Threading;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Binance message processing logging handler.<para/>
    /// A middlewear to listen and log any request or response.
    /// </summary>
    public class BinanceLoggingHandler : MessageProcessingHandler
    {
        private ILogger logger;

        public BinanceLoggingHandler(ILogger logger)
        : base(new HttpClientHandler())
        {
            this.logger = logger;
        }

        public BinanceLoggingHandler(ILogger logger, HttpMessageHandler innerHandler)
        : base(innerHandler)
        {
            this.logger = logger;
        }

        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.logger.LogTrace(request.ToString());

            if (null != request.Content)
            {
                this.logger.LogTrace(request.Content.ReadAsStringAsync().Result);
            }

            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            this.logger.LogTrace(response.ToString());

            if (null != response.Content)
            {
                this.logger.LogTrace(response.Content.ReadAsStringAsync().Result);
            }

            return response;
        }
    }
}