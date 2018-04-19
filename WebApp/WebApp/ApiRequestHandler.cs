using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Framework.Logging;
using Framework.Models;
using Framework.Utils;

namespace WebApp
{
    public class ApiRequestHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var guid = Guid.NewGuid();

            LoggingQueue.Add(new LogMessage
            {
                LogLevel = LogLevel.Log,
                Message = $"Request coming in with Guid: {guid.ToString()}",
                TimeStamp = DateTimeExtensions.BeiJingNow(),
                RequestUrl = request.RequestUri.ToString(),
                PayLoad = request.RequestUri.ToString().Contains("image") ? "" : request.Content?.ReadAsStringAsync().Result ?? ""
            });

            var response = await base.SendAsync(request, cancellationToken);

            LoggingQueue.Add(new LogMessage
            {
                LogLevel = LogLevel.Log,
                Message = $"Request going out with Guid: {guid.ToString()}",
                TimeStamp = DateTimeExtensions.BeiJingNow(),
                RequestUrl = request.RequestUri.ToString(),
                PayLoad = response.Content?.ReadAsStringAsync().Result ?? ""
            });

            return response;
        }
    }
}