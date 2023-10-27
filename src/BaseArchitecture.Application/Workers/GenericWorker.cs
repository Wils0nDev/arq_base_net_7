using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Interfaces.Workers;
using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BaseArchitecture.Application.Workers
{
    public class GenericWorker : IGenericWorker
    {
        private readonly ILogger<GenericWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;

        public GenericWorker(ILogger<GenericWorker> logger, IServiceProvider serviceProvider,
                            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _unitOfWork = _serviceProvider.GetService<IUnitOfWork>();

            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
                TraceIdentifier = httpContextAccessor.HttpContext.TraceIdentifier;
            else
                TraceIdentifier = Guid.NewGuid().ToString();

            this.NameJob = "Anonymous";
        }

        public TimeSpan? Delay { get; set; } = null;
        public string NameJob { get; set; }
        public string TraceIdentifier { get; set; }

        /// <summary>
        /// Función que se va a ejecutar en segundo plano
        /// </summary>
        public Func<IServiceProvider, Task> RunFunction { get; set; } = null;

        /// <summary>
        /// Función para interceptar el error producido por la tarea en segundo plano.
        /// </summary>
        public Func<IServiceProvider, Exception, Task> RunFunctionException { get; set; } = null;


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Exception innerException = null;
            Stopwatch stopwatch = new Stopwatch();

            try
            {

                try
                {

                    _logger.LogInformation($"Inicio de proceso en segundo plano {nameof(GenericWorker)}.");
                    var now = DateTimeOffset.Now;
                    var enqueued = new LogJob
                    {
                        NameJob = NameJob,
                        StateJob = StateJob.Enqueued,
                        CreateDateOnly = now.DateTime,
                        CreateDate = now.DateTime,
                        TraceIdentifier = TraceIdentifier
                    };
                    await _unitOfWork.LogJobs.CreateAsync(enqueued);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);


                    if (Delay.HasValue)
                    {
                        _logger.LogInformation($"Se detuvo el proceso con un Delay '{Delay.Value:hh\\:mm\\:ss}'");
                        await Task.Delay(Delay.Value, cancellationToken);
                        _logger.LogInformation($"Se continua el proceso en segundo plano.");
                    }


                    now = DateTimeOffset.Now;
                    await _unitOfWork.LogJobs.CreateAsync(new LogJob
                    {
                        NameJob = NameJob,
                        StateJob = StateJob.Processing,
                        CreateDateOnly = now.DateTime,
                        CreateDate = now.DateTime,
                        TraceIdentifier = TraceIdentifier
                    });
                    await _unitOfWork.SaveChangesAsync(cancellationToken);


                    // Lógica de la tarea programada
                    stopwatch.Start();
                    await RunFunction.Invoke(_serviceProvider);
                    stopwatch.Stop();



                    _logger.LogInformation($"Fin del proceso en segundo plano {nameof(GenericWorker)}.");

                    now = DateTimeOffset.Now;
                    await _unitOfWork.LogJobs.CreateAsync(new LogJob
                    {
                        NameJob = NameJob,
                        StateJob = StateJob.Succeeded,
                        CreateDateOnly = now.DateTime,
                        CreateDate = now.DateTime,
                        Duration = stopwatch.Elapsed,
                        TraceIdentifier = TraceIdentifier
                    });
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
                catch (Exception ex) when (RunFunctionException != null)
                {
                    innerException = ex;
                    _logger.LogInformation("El cliente intercepto el error generado en su proceso de segundo plano.");
                    await RunFunctionException.Invoke(_serviceProvider, ex);
                    _logger.LogInformation("Se finalizo el tratamiento de error de segundo plano.");


                    var failed = new LogJob
                    {
                        NameJob = NameJob,
                        StateJob = StateJob.Failed,
                        CreateDateOnly = DateTime.Now,
                        CreateDate = DateTime.Now,
                        Exception = ex.Message,
                        StackTrace = ex.ToString(),
                        Duration = stopwatch.Elapsed,
                        TraceIdentifier = TraceIdentifier
                    };
                    if (ex.InnerException != null)
                        failed.InnerException = ex.InnerException.Message;

                    await _unitOfWork.LogJobs.CreateAsync(failed);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }
                catch (Exception ex) when (RunFunctionException == null)
                {
                    innerException = ex;
                    _logger.LogError(ex, "Error no controlado de la tarea en segundo plano");

                    var failed = new LogJob
                    {
                        NameJob = NameJob,
                        StateJob = StateJob.Failed,
                        CreateDateOnly = DateTime.Now,
                        CreateDate = DateTime.Now,
                        Exception = ex.Message,
                        StackTrace = ex.ToString(),
                        Duration = stopwatch.Elapsed,
                        TraceIdentifier = TraceIdentifier
                    };
                    if (ex.InnerException != null)
                        failed.InnerException = ex.InnerException.Message;

                    await _unitOfWork.LogJobs.CreateAsync(failed);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                }


            }
            catch (Exception ex)
            {
                var customException = new AggregateException("Ocurrió uno o más errores.", innerException, ex);
                _logger.LogCritical(customException, "Ocurrieron uno o más errores.");

                var failed = new LogJob
                {
                    NameJob = NameJob,
                    StateJob = StateJob.Failed,
                    CreateDateOnly = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Exception = customException.Message,
                    StackTrace = customException.ToString(),
                    Duration = stopwatch.Elapsed,
                    TraceIdentifier = TraceIdentifier
                };

                failed.InnerException = innerException.Message;

                await _unitOfWork.LogJobs.CreateAsync(failed);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }
            finally
            {
                await StopAsync(cancellationToken);
            }


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Se detuvo el proceso en segundo plano {nameof(GenericWorker)}.");


            return Task.CompletedTask;
        }

    }

}
