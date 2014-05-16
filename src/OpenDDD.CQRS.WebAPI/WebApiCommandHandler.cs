using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenDDD.CQRS.WebAPI
{
    public class WebApiCommandHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (request.Method.Method == "GET")

            return await base.SendAsync(request, cancellationToken);
        }
    }
}