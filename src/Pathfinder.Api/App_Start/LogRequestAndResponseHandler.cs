using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Pathfinder.Utilities;

namespace Pathfinder.Api
{
    internal class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage pRequest, CancellationToken pCancellationToken)
        {
            // log request body
            string requestBody = await pRequest.Content.ReadAsStringAsync();
            LogTo.Verbose("requestBody|{requestBody}", requestBody);

            // let other handlers process the request
            var result = await base.SendAsync(pRequest, pCancellationToken);

            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                LogTo.Verbose("responseBody|{responseBody}", responseBody);
            }

            return result;
        }
    }
}
