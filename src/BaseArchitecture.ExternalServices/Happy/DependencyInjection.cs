using System.Net;
using BaseArchitecture.ExternalServices.Happy.Workers;
using BaseArchitecture.ExternalServices.Happy.Base;
using BaseArchitecture.ExternalServices.Happy.EndPoint;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.ExternalServices.Happy
{
    public static class Extensions
    {

        /// <summary>
        /// Agregamos las conexiones para las apis de Happy Services de Antamina.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddServiceHappy(this IServiceCollection services, Func<ServiceHappyOptions> options)
        {

            var option = options.Invoke();
            services.AddSingleton(option);
            services.AddHttpContextAccessor();
            var result = services.AddHttpClient<IApiServiceHappy, ApiServiceHappy>(client =>
            {
                client.BaseAddress = new Uri(option.Uri);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.DefaultRequestHeaders.Clear();
            });
            services.AddSingleton<IAuthentication, Authentication>();
            services.AddSingleton<ISecurityServices, SecurityServices>();
            services.AddSingleton<IToken, Token>();
            services.AddSingleton<IAws, Aws>();
            services.AddHostedService<WorkerCognitoToken>();

            return result;
        }
    }
}