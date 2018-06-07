using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication4.ViewModel;

namespace WebApplication4.Infrastructure
{
    public class ErrorActionResult : IHttpActionResult
    {
        private HttpResponseMessage _response;

        public ErrorActionResult(HttpRequestMessage request, HttpStatusCode statusCode, ErrorsModel errors)
        {
            _response = request.CreateResponse(statusCode, errors);
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }

        public HttpResponseMessage GetResponse()
        {
            return _response;
        }
    }
}