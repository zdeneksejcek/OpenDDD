
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenDDD.CQRS.WebAPI
{
    public class WebApiCommandStatusHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
