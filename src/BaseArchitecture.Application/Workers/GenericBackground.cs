using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Domain.Enums;
using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Threading;

namespace BaseArchitecture.Application.Workers
{
    public class GenericBackground : BackgroundService
    {
        private IUnitOfWork _unitOfWork;
        private CronExpression _cronExpression;
        private IServiceProvider _serviceProvider;
        private string _nameJob;

        /// <summary>
        /// Función que se va a ejecutar en segundo plano
        /// </summary>
        protected Func<IServiceProvider, Task> RunFunction { get; set; }


        /// <summary>
        /// Función para interceptar el error producido por la tarea en segundo plano.
        /// </summary>
        public Func<IServiceProvider, Exception, Task> RunFunctionException { get; set; } = null;

        public GenericBackground(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
        }

        /// <summary>
        /// Inicializar la clase base.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="cronExpression">Frecuencia de ejecución de tarea en segundo plano</param>
        /// <param name="nameJob">Nombre de la tarea en segundo plano</param>
        protected void Initialize(CronExpression cronExpression, string nameJob)
        {
            _unitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            _cronExpression = cronExpression;
            _nameJob = nameJob;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Exception innerException = null;
            while (!cancellationToken.IsCancellationRequested)
            {
                Stopwatch stopwatch = new Stopwatch();
                var guid = Guid.NewGuid();

                try
                {
                    try
                    {
                        var now = DateTimeOffset.Now;
                        var enqueued = new LogJob
                        {
                            NameJob = _nameJob,
                            StateJob = StateJob.Enqueued,
                            CreateDateOnly = now.DateTime,
                            CreateDate = now.DateTime,
                            TraceIdentifier = guid.ToString()
                        };
                        await _unitOfWork.LogJobs.CreateAsync(enqueued);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);


                        var nextSchedule = _cronExpression.GetNextOccurrence(now, TimeZoneInfo.Local);
                        var delay = nextSchedule - now;
                        if (delay.HasValue && delay > TimeSpan.Zero)
                            await Task.Delay(delay.Value, cancellationToken);


                        now = DateTimeOffset.Now;
                        await _unitOfWork.LogJobs.CreateAsync(new LogJob
                        {
                            NameJob = _nameJob,
                            StateJob = StateJob.Processing,
                            CreateDateOnly = now.DateTime,
                            CreateDate = now.DateTime,
                            TraceIdentifier = guid.ToString()
                        });
                        await _unitOfWork.SaveChangesAsync(cancellationToken);



                        // Lógica de la tarea programada
                        stopwatch.Start();
                        await RunFunction.Invoke(_serviceProvider);
                        stopwatch.Stop();


                        now = DateTimeOffset.Now;
                        await _unitOfWork.LogJobs.CreateAsync(new LogJob
                        {
                            NameJob = _nameJob,
                            StateJob = StateJob.Succeeded,
                            CreateDateOnly = now.DateTime,
                            CreateDate = now.DateTime,
                            Duration = stopwatch.Elapsed,
                            TraceIdentifier = guid.ToString()
                        });
                        await _unitOfWork.SaveChangesAsync(cancellationToken);

                    }
                    catch (Exception ex) when (RunFunctionException != null)
                    {
                        innerException = ex;
                        await RunFunctionException.Invoke(_serviceProvider, ex);

                        var failed = new LogJob
                        {
                            NameJob = _nameJob,
                            StateJob = StateJob.Failed,
                            CreateDateOnly = DateTime.Now,
                            CreateDate = DateTime.Now,
                            Exception = ex.Message,
                            StackTrace = ex.ToString(),
                            Duration = stopwatch.Elapsed,
                            TraceIdentifier = guid.ToString()
                        };
                        if (ex.InnerException != null)
                            failed.InnerException = ex.InnerException.Message;

                        await _unitOfWork.LogJobs.CreateAsync(failed);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }
                    catch (Exception ex) when (RunFunctionException == null)
                    {
                        innerException = ex;

                        var failed = new LogJob
                        {
                            NameJob = _nameJob,
                            StateJob = StateJob.Failed,
                            CreateDateOnly = DateTime.Now,
                            CreateDate = DateTime.Now,
                            Exception = ex.Message,
                            StackTrace = ex.ToString(),
                            Duration = stopwatch.Elapsed,
                            TraceIdentifier = guid.ToString()
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
                    //_logger.LogCritical(customException, "Ocurrió uno o más errores.");

                    var failed = new LogJob
                    {
                        NameJob = _nameJob,
                        StateJob = StateJob.Failed,
                        CreateDateOnly = DateTime.Now,
                        CreateDate = DateTime.Now,
                        Exception = customException.Message,
                        StackTrace = customException.ToString(),
                        Duration = stopwatch.Elapsed,
                        TraceIdentifier = guid.ToString()
                    };
                    failed.InnerException = innerException.Message;

                    await _unitOfWork.LogJobs.CreateAsync(failed);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    
                }

            }

        }



    }
}
