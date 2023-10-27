using BaseArchitecture.Application.Interfaces.Repositories;
using Cronos;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Application.Workers
{
    public class AuditEndpointBackground : GenericBackground
    {
        public AuditEndpointBackground(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            //base.Initialize(CronExpression.Parse("*/1 * * * *"), nameof(AuditEndpointBackground)); // Cada minuto
            base.Initialize(CronExpression.Parse("0 2 * * *"), nameof(AuditEndpointBackground)); // Ejecutar a las 02:00:00 todos los días           
            this.RunFunction = (service) => Prueba(service);

            this.RunFunctionException = async (service, exception) => {

                var repository = serviceProvider.GetService<IAuditEndpointRepository>();
                await repository.DeleteOlds();

                //var numerador = 1;
                //var denominador = 0;
                //var dividendo = numerador / denominador;

            };

        }

        public async Task Prueba(IServiceProvider serviceProvider)
        {
            var repository = serviceProvider.GetService<IAuditEndpointRepository>();
            await repository.DeleteOlds();

            //var numerador = 1;
            //var denominador = 0;
            //var dividendo = numerador/denominador;

        }


    }
}
