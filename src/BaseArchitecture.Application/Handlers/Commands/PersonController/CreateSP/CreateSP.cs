using AutoMapper;
using BaseArchitecture.Application.Handlers.Commands.PersonController.Create;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Common.Helpers;
using MediatR;
using Reec.Inspection;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.CreateSP
{
    public class CreateSP : IRequest<ReecMessage>
    {
        public CreateSPRequest Model { get; }
        public CreateSP(CreateSPRequest model)
        {
            Model = model;
        }

        public class CreateSPHandler : IRequestHandler<CreateSP, ReecMessage>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly HeaderToken _token;
            private readonly IPersonRepository _personServices;

            public CreateSPHandler(IUnitOfWork unitOfWork, IMapper mapper, HeaderToken token, IPersonRepository personServices)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _token = token;
                _personServices = personServices;
            }
            public async Task<ReecMessage> Handle(CreateSP request, CancellationToken cancellationToken)
            {
                var isExists = _unitOfWork.Persons.AsNoTracking().Any(t => t.DNI == request.Model.Person.DNI && t.RecordStatus == ConstantBase.Active);
                if (isExists)
                    throw new ReecException(ReecEnums.Category.Warning, $"La persona que intenta crear ya existe.");

                await _personServices.CreateAndUpdateByStoredProcedure(request.Model.Person);
                var message = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.CreateMessage);
                return message;
            }
        }
    }
}
