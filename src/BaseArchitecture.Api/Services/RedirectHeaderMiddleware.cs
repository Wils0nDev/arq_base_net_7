using BaseArchitecture.ExternalServices.Happy.EndPoint;
using BaseArchitecture.ExternalServices.Happy.Models;

namespace BaseArchitecture.Api.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RedirectHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthentication _authentication;

        public RedirectHeaderMiddleware(RequestDelegate next,
                                            IAuthentication authentication)
        {
            this._next = next;
            this._authentication = authentication;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            var c = httpContext.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (c[1] != "Execute")
            {
                var code = httpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "code");
                var header = httpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "header");

                var response = await this._authentication.GetDeserializeObject(
                                new EncryptRequest { Code = code.Value, TextTransform = header.Value });
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RedirectHeaderMiddlewareExtensions
    {

        /// <summary>
        /// Valida si las peticiones Request tiene los parametros de cabecera "Code" y Header"
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRedirectHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RedirectHeaderMiddleware>();
        }
    }
}

