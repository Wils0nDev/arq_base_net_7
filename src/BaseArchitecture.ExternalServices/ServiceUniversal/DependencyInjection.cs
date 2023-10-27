using BaseArchitecture.ExternalServices.ServiceUniversal.Base;
using BaseArchitecture.ExternalServices.ServiceUniversal.EndPoint;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BaseArchitecture.ExternalServices.ServiceUniversal
{
    public static class Extensions
    {
        /// <summary>
        /// Agregamos las conexion para el servicio universal
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddServiceUniversal(this IServiceCollection services, Func<ServiceUniversalOptions> options)
        {
            var option = options.Invoke();
            services.AddSingleton(option);
            var result = services.AddHttpClient<IApiServiceUniversal, ApiServiceUniversal>(client =>
            {
                client.BaseAddress = new Uri(option.Uri);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Clear();
            });

            services.AddSingleton<IServiceUniversal, EndPoint.ServiceUniversal>();

            return result;
        }
    }
}
