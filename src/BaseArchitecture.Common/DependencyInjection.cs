using BaseArchitecture.Common.Excel;
using Microsoft.Extensions.DependencyInjection;



namespace BaseArchitecture.Common
{
    public static class Extensions
    {

        /// <summary>
        /// Agregamos los servicios comunes de generación de excel, lectura de excel.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            //RulesEngine d = new RulesEngine();

            services.AddSingleton<IGenerateExcel, GenerateExcel>();
            services.AddSingleton<IExcelClosedXml, ExcelClosedXml>();
            //services.AddTransient<RulesEngine.Interfaces.IRulesEngine, RulesEngine.RulesEngine>();
            

            return services;
        }
    }
}
