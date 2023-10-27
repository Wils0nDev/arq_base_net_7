using BaseArchitecture.Application.Interfaces.Repositories;
using Cronos;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Application.Workers
{
    public class AuditHttpBackground : GenericBackground
    {
        public AuditHttpBackground(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

            //base.Initialize(CronExpression.Parse("*/1 * * * *"), nameof(AuditHttpBackground)); // Cada minuto
            base.Initialize(CronExpression.Parse("0 2 * * *"), nameof(AuditHttpBackground)); // Ejecutar a las 02:00:00 todos los días           
            this.RunFunction = async (service) =>
            {
                var repository = service.GetService<IAuditHttpRepository>();
                await repository.DeleteOlds();
            };

        }



    }
}
