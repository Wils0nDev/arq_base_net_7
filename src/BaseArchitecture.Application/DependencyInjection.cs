using BaseArchitecture.Application.Interfaces.Workers;
using BaseArchitecture.Application.Workers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Reflection;


namespace BaseArchitecture.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Interfaz de procesos en segundo plano.
            services.AddTransient<IGenericWorker, GenericWorker>();
            services.AddCustomHostedService();


            //services.AddHostedService<LogJobBackground>();
            //services.AddHostedService<LogDbBackground>();
            //services.AddHostedService<LogHttpBackground>();
            //services.AddHostedService<AuditHttpBackground>();
            //services.AddHostedService<AuditEndpointBackground>();
            return services;
        }


        /// <summary>
        /// Agregamos todos los servicios en segundo plano de forma dinámina
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomHostedService(this IServiceCollection services)
        {
            var dll = typeof(GenericBackground).Assembly;
            var types = dll.GetTypes().Where(x => x.BaseType == typeof(GenericBackground)).ToList();
            foreach (var item in types)            
                services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IHostedService), item));            
            return services;
        }


    }
}