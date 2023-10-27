using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.ExternalServices.AwsS3;
using BaseArchitecture.ExternalServices.Happy;
using BaseArchitecture.ExternalServices.Mail;
using BaseArchitecture.ExternalServices.ServiceUniversal;
using BaseArchitecture.Infrastructure.Persistence;
using BaseArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;

namespace BaseArchitecture.Infrastructure
{
    public static class Extensions
    {

        /// <summary>
        /// Agregamos la infraestructura de la aplicación.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <param name="IsLocal"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration, bool IsLocal)
        {

            services.AddDbContext<BaseDbContext>(opstions =>
            {
                opstions.UseSqlServer(Configuration.GetConnectionString("DEV_STANDAR"));
                if (IsLocal)
                {
                    opstions.LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging();
                }
            });

            //agregamos todos los repositoprios genéricos en una sola linea.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<HeaderToken>((se) =>
            {
                var _HttpContext = se.GetRequiredService<IHttpContextAccessor>();
                if (_HttpContext.HttpContext != null)
                {
                    var request = _HttpContext.HttpContext.Request.Headers;
                    var header = request.FirstOrDefault(x => x.Key.ToLower() == "headertoken");

                    if (!string.IsNullOrWhiteSpace(header.Value))
                    {
                        var decodedHeaderToken = HttpUtility.HtmlDecode(header.Value);
                        var result = JsonConvert.DeserializeObject<HeaderToken>(decodedHeaderToken);
                        return result;
                    }
                }
                return null;
            });


            services.AddScoped<ILogDbRepository, LogDbRepository>();
            services.AddScoped<ILogJobRepository, LogJobRepository>();
            services.AddScoped<ILogHttpRepository, LogHttpRepository>();
            services.AddScoped<IAuditHttpRepository, AuditHttpRepository>();
            services.AddScoped<IAuditEndpointRepository, AuditEndpointRepository>();


            services.AddScoped<IMasterTableRepository, MasterTableRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();


            //TODO: Solo sirve de ejemplo de errores
            services.AddScoped<IDemoLogRepository, DemoLogRepository>();


            return services;
        }


        public static IServiceCollection AddInfrastructureExternalServices(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddArrobaAntamina(() =>
            {
                return Configuration.GetSection(nameof(ServiceArrobaOptions))
                                .Get<ServiceArrobaOptions>();
            }).AddPolicyHandler((serviceProvider, httpRequestMessage) =>
            CustomPolly.GetRetryPolicy(serviceProvider, httpRequestMessage));


            services.AddServiceHappy(() =>
            {
                return Configuration.GetSection(nameof(ServiceHappyOptions))
                                .Get<ServiceHappyOptions>();
            }).AddPolicyHandler((serviceProvider, httpRequestMessage) =>
            CustomPolly.GetRetryPolicy(serviceProvider, httpRequestMessage));


            services.AddAwsS3Antamina(() =>
            {
                return Configuration.GetSection(nameof(AwsS3AntaminaOptions))
                                .Get<AwsS3AntaminaOptions>();
            });


            services.AddServiceUniversal(() =>
            {
                return Configuration.GetSection(nameof(ServiceUniversalOptions))
                                .Get<ServiceUniversalOptions>();
            });
           
            return services;
        }


    }
}
