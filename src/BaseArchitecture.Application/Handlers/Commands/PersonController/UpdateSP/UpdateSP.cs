using AutoMapper;
using BaseArchitecture.Application.Handlers.Commands.PersonController.Update;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Common.Helpers;
using MediatR;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.UpdateSP
{
    public class UpdateSP : IRequest<ReecMessage>
    {
        public UpdateSPRequest Model { get; }
        public UpdateSP(UpdateSPRequest model)
        {
            Model = model;
        }

        public class UpdateSPHandler : IRequestHandler<UpdateSP, ReecMessage>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly HeaderToken _token;
            private readonly IPersonRepository _personRepository;

            public UpdateSPHandler(IUnitOfWork unitOfWork, IMapper mapper, HeaderToken token, IPersonRepository personRepository)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _token = token;
                _personRepository = personRepository;
            }
            public async Task<ReecMessage> Handle(UpdateSP request, CancellationToken cancellationToken)
            {
                await _personRepository.CreateAndUpdateByStoredProcedure(request.Model.Person);
                var message = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.CreateMessage);
                return message;
            }
        }
    }
}
