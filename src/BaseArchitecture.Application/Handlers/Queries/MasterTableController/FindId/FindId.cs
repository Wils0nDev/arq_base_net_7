using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindId
{
    public class FindId : IRequest<ModelMasterTable>
    {
        public string IdMasterTable { get; }
        public FindId(string idMasterTable)
        {
            IdMasterTable = idMasterTable;
        }

        public class FindIdHandler : IRequestHandler<FindId, ModelMasterTable>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public FindIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ModelMasterTable> Handle(FindId request, CancellationToken cancellationToken)
            {
                var entity = await _unitOfWork.MasterTables.FindAsync(request.IdMasterTable);
                if (entity != null)
                    return _mapper.Map<ModelMasterTable>(entity);

                return null;
            }
        }


    }
}
