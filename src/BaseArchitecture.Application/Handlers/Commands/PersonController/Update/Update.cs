using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Common.Helpers;
using BaseArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.Update
{
    public class Update : IRequest<ReecMessage>
    {
        public UpdateRequest Model { get; }
        public Update(UpdateRequest model)
        {
            Model = model;
        }

        public class UpdateHandler : IRequestHandler<Update, ReecMessage>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ReecMessage> Handle(Update request, CancellationToken cancellationToken)
            {
                using var transaccion = await _unitOfWork.TransactionAsync(cancellationToken);

                var isExists = _unitOfWork.Persons.AsNoTracking().Any(t => t.IdPerson == request.Model.Person.IdPerson);
                if (!isExists)
                    throw new ReecException(ReecEnums.Category.Warning, $"No exíste el IdPersona para actualizar.");

                var maturityDate = DateTime.Now.Date.AddYears(ConstantBase.AgeMaturity * -1);

                var isMature = request.Model.Person.Birthday.Value.Date <= maturityDate;

                if (!isMature)
                    throw new ReecException(ReecEnums.Category.Warning, $"La persona debe ser mayor de edad.");

                var entity = _mapper.Map<Person>(request.Model.Person);
                var entry = _unitOfWork.Persons.Update(entity);

                if (request.Model.ListAttachedFile != null || request.Model.ListAttachedFile.Count > 0)
                {
                    foreach (var item in request.Model.ListAttachedFile)
                    {
                        var attachedFile = _mapper.Map<AttachedFile>(item);
                        var personFile = new PersonFile
                        {
                            IdPerson = request.Model.Person.IdPerson,
                            IdAttachedFile = attachedFile.IdAttachedFile,
                            RecordStatus = attachedFile.RecordStatus
                        };

                        switch (item.StatusFile)
                        {
                            case BaseEnums.StatusFile.New:
                                _unitOfWork.AttachedFiles.Create(attachedFile);
                                _unitOfWork.PersonFiles.Create(personFile); break;
                            case BaseEnums.StatusFile.NoTouch:
                                break;
                            case BaseEnums.StatusFile.Delete:
                                _unitOfWork.AttachedFiles.Delete(attachedFile);
                                _unitOfWork.PersonFiles.Delete(personFile); break;
                            default:
                                break;
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaccion.CommitAsync(cancellationToken);

                var message = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.UpdateMessage);
                return message;
            }
        }
    }

}
