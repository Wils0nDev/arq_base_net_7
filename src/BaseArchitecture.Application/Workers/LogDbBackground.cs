using BaseArchitecture.Application.Interfaces.Repositories;
using Cronos;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Application.Workers
{
    public class LogDbBackground : GenericBackground
    {

        public LogDbBackground(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //base.Initialize(CronExpression.Parse("*/1 * * * *"), nameof(LogDbBackground)); // Cada minuto
            base.Initialize(CronExpression.Parse("0 2 * * *"), nameof(LogDbBackground)); // Ejecutar a las 02:00:00 todos los días           
            this.RunFunction = async (service) =>
            {
                var repository = service.GetService<ILogDbRepository>();
                await repository.DeleteOlds();
            };

        }

    }
}
