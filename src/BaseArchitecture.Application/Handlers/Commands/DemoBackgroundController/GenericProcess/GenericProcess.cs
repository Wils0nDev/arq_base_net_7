using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Interfaces.Workers;
using BaseArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reec.Inspection;

namespace BaseArchitecture.Application.Handlers.Commands.DemoBackgroundController.GenericProcess
{
    public class GenericProcess : IRequest<ReecMessage>
    {
        public GenericProcessRequest Model { get; }

        public GenericProcess(GenericProcessRequest model)
        {
            Model = model;
        }

        public class GenericProcessHandler : IRequestHandler<GenericProcess, ReecMessage>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IGenericWorker _genericWorker;

            public GenericProcessHandler(IUnitOfWork unitOfWork, IGenericWorker background)
            {
                _unitOfWork = unitOfWork;
                _genericWorker = background;

            }

            public async Task<ReecMessage> Handle(GenericProcess request, CancellationToken cancellationToken)
            {

                var prueba = await _unitOfWork.MasterTables.AsNoTracking().ToListAsync(cancellationToken);

                _genericWorker.Delay = TimeSpan.FromSeconds(2);
                //_genericWorker.RunFunction = async (service) =>
                //{
                //    var uow = service.GetService<IUnitOfWork>();
                //    var listPerson = await uow.Persons.AsNoTracking().ToListAsync();
                //    var demoLogRepository = service.GetService<IDemoLogRepository>();

                //    var numerador = 1;
                //    var denominador = 0;
                //    var resultado = numerador / denominador;

                //};
                _genericWorker.RunFunction = (service) => RunFunctionAsync(service);
                _ = _genericWorker.StartAsync(cancellationToken);


                _genericWorker.RunFunctionException = async (service, ex) =>
                {
                    var uow = service.GetService<IUnitOfWork>();
                    var listPerson = await uow.Persons.AsNoTracking().ToListAsync();

                    //enviar correo a alguien,
                    //aplicar un rollback

                    var numerador = 1;
                    var denominador = 0;
                    var dividendo = numerador / denominador;

                };

                var response = new ReecMessage(ReecEnums.Category.OK, "Se envio a procesar en segundo plano.");
                return response;
            }

            public async Task RunFunctionAsync(IServiceProvider service)
            {
                var uow = service.GetService<IUnitOfWork>();
                var listPerson = await uow.Persons.AsNoTracking().ToListAsync();
                var demoLogRepository = service.GetService<IDemoLogRepository>();


                


                List<Person> listaMasiva = new List<Person>();
                for (int i = 0; i < 10000; i++)
                {
                    Person p = new Person()
                    {
                        IdPerson = Guid.NewGuid(),
                        UserRecordCreation = "Masivo",
                        RecordCreationDate= DateTime.Now,
                        RecordStatus = "A"
                    };
                    listaMasiva.Add(p);
                }


                await uow.BulkInsertAsync(listaMasiva);



            }
        }


    }

}
