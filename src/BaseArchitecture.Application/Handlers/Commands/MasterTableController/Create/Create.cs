using AutoMapper;
using MediatR;
using BaseArchitecture.Application.Interfaces.Repositories;
using Ent = BaseArchitecture.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reec.Inspection;
using BaseArchitecture.Application.Models.Database;

namespace BaseArchitecture.Application.Handlers.Commands.MasterTableController.Create
{
    public class Create : IRequest<ModelMasterTable>
    {
        public ModelMasterTable ModelMasterTable { get; }
        public Create(ModelMasterTable modelMasterTable)
        {
            ModelMasterTable = modelMasterTable;
        }


        public class CreateHandler : IRequestHandler<Create, ModelMasterTable>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ModelMasterTable> Handle(Create request, CancellationToken cancellationToken)
            {
                var isExists = await _unitOfWork.MasterTables.FindAsync(request.ModelMasterTable.IdMasterTable);
                if (isExists != null)
                    throw new ReecException(ReecEnums.Category.Warning, "No puede agregar un registro duplicado.");

                var entity = _mapper.Map<Ent.MasterTable>(request.ModelMasterTable);
                entity.RecordCreationDate = DateTime.Now;

                _unitOfWork.MasterTables.Create(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<ModelMasterTable>(entity);

                return result;
            }
        }
    }

}
