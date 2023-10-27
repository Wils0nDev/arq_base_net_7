using BaseArchitecture.ExternalServices.Mail.Base;
using BaseArchitecture.ExternalServices.Mail.EndPoint;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BaseArchitecture.ExternalServices.Mail
{
    public static class Extensions
    {
        /// <summary>
        /// Agregamos las conexiones para las apis de se de Antamina.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddArrobaAntamina(this IServiceCollection services, Func<ServiceArrobaOptions> options)
        {

            var option = options.Invoke();
            services.AddSingleton(option);
            var result = services.AddHttpClient<IApiArroba, ApiArroba>(client =>
            {
                client.BaseAddress = new Uri(option.Uri);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Clear();
            });

            services.AddSingleton<IMail, EndPoint.Mail>();

            return result;
        }
    }
}
