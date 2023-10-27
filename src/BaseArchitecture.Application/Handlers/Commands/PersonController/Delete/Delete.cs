using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Common.Helpers;
using BaseArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.Delete
{
    public class Delete : IRequest<ReecMessage>
    {
        public Guid IdPerson { get; }
        public Delete(Guid idPerson)
        {
            IdPerson = idPerson;
        }

        public class DeleteHandler : IRequestHandler<Delete, ReecMessage>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ReecMessage> Handle(Delete request, CancellationToken cancellationToken)
            {
                var entity = await _unitOfWork.Persons.AsNoTracking().Where(p => p.IdPerson == request.IdPerson).FirstOrDefaultAsync(cancellationToken);

                if (entity == null)
                    throw new ReecException(ReecEnums.Category.BusinessLogic, ConstantMessage.NotExists);

                var attachedFilesIds = _unitOfWork.PersonFiles.AsNoTracking().Where(pf => pf.IdPerson == request.IdPerson).Select(pf => pf.IdAttachedFile).ToList();
                var attachedFiles = _unitOfWork.AttachedFiles.AsNoTracking().Where(af => attachedFilesIds.Contains(af.IdAttachedFile)).ToList();

                foreach(var af in attachedFiles)
                {
                    af.RecordStatus = ConstantBase.Inactive;
                    _unitOfWork.AttachedFiles.Update(af);
                }

                var PersonFiles = _unitOfWork.PersonFiles.AsNoTracking().Where(pf => pf.IdPerson == request.IdPerson).ToList();

                foreach (var PersonFile in PersonFiles)
                {
                    PersonFile.RecordStatus = ConstantBase.Inactive;
                    _unitOfWork.PersonFiles.Update(PersonFile);
                }

                entity.RecordStatus = ConstantBase.Inactive;
                _unitOfWork.Persons.Update(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var message = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.DeleteMessage);
                return message;
            }

        }
    }

}

