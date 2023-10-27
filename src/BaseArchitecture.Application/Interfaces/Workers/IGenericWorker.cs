using Microsoft.Extensions.Hosting;

namespace BaseArchitecture.Application.Interfaces.Workers
{
    /// <summary>
    /// Interfaz personalizada para procesamiento en segundo plano.
    /// </summary>
    public interface IGenericWorker : IHostedService
    {
        /// <summary>
        /// Nombre de tarea en segundo plano. Valor por defecto 'Anonymous'
        /// </summary>
        string NameJob { get; set; }

        /// <summary>
        /// Trace de identificación para procesos en segundo plano.
        /// Valor por defecto HttpContext.TraceIdentifier o Guid.NewGuid().ToString()
        /// </summary>
        string TraceIdentifier { get; set; } 

        /// <summary>
        /// Tiempo de espera antes de ejecutar el proceso en segundo plano.
        /// </summary>
        TimeSpan? Delay { get; set; }

        /// <summary>
        /// Función anónima en segundo plano.
        /// </summary>
        Func<IServiceProvider, Task> RunFunction { get; set; }

        /// <summary>
        /// Función anónima para capturar una excepción generada por el proceso en segundo plano.
        /// </summary>
        Func<IServiceProvider, Exception, Task> RunFunctionException { get; set; }

    }

}
