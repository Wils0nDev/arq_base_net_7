using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BaseArchitecture.Application.Handlers.Commands.DemoLogController.InternalServerError_SP
{
    public class InternalServerError_SP : IRequest<string>
    {

        public class InternalServerError_SPHandler : IRequestHandler<InternalServerError_SP, string>
        {
            private readonly IDemoLogRepository _demoLogRepository;
            private readonly IUnitOfWork _unitOfWork;

            public InternalServerError_SPHandler(IDemoLogRepository demoLogRepository, IUnitOfWork unitOfWork)
            {
                this._demoLogRepository = demoLogRepository;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(InternalServerError_SP request, CancellationToken cancellationToken)
            {

                //List<Person> ListPersons = new List<Person>();
                //for (int i = 0; i < 1000; i++)
                //{
                //    ListPersons.Add(
                //        new Person
                //        {
                //            IdPerson = Guid.NewGuid(),
                //            UserRecordCreation = "DEMO",
                //            RecordCreationDate = DateTime.Now,
                //            RecordStatus = "A"
                //        });
                //}
                //await this._unitOfWork.BulkInsertAsync(ListPersons, null, cancellationToken);


                return await _demoLogRepository.InternalServerError_SP();
            }

        }

    }


}
