using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Common.Helpers;
using BaseArchitecture.Domain.Entities;
using MediatR;
using Reec.Inspection;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.Create
{
    public class Create : IRequest<ReecMessage>
    {
        public CreateRequest Model { get; }
        public Create(CreateRequest createRequest)
        {
            Model = createRequest;
        }

        public class CreateHandler : IRequestHandler<Create, ReecMessage>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly HeaderToken _token;

            public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper, HeaderToken token)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _token = token;
            }
            public async Task<ReecMessage> Handle(Create request, CancellationToken cancellationToken)
            {
                using var transaction = await _unitOfWork.TransactionAsync(cancellationToken);
                var isExists = _unitOfWork.Persons.AsNoTracking().Any(t => t.DNI == request.Model.Person.DNI && t.RecordStatus == ConstantBase.Active);
                if (isExists)
                    throw new ReecException(ReecEnums.Category.Warning, $"La persona que intenta crear ya existe.");

                var maturityDate = DateTime.Now.Date.AddYears(ConstantBase.AgeMaturity * -1);

                var isMature = request.Model.Person.Birthday.Value.Date <= maturityDate;

                if(!isMature)
                    throw new ReecException(ReecEnums.Category.Warning, $"La persona debe ser mayor de edad.");

                var person = _mapper.Map<Person>(request.Model.Person);
                _unitOfWork.Persons.Create(person);

                if (request.Model.ListAttachedFile != null && request.Model.ListAttachedFile.Count > 0)
                {
                    var attachedFiles = _mapper.Map<List<AttachedFile>>(request.Model.ListAttachedFile);
                    _unitOfWork.AttachedFiles.CreateRange(attachedFiles);

                    var personFiles = attachedFiles.Select(s => new PersonFile
                    {
                        IdAttachedFile = s.IdAttachedFile,
                        IdPerson = person.IdPerson,
                        RecordStatus = ConstantBase.Active,
                    }).ToList();

                    _unitOfWork.PersonFiles.CreateRange(personFiles);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                var message = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.CreateMessage);
                return message;
            }
        }
    }

}
