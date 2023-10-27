using AutoMapper;
using BaseArchitecture.Application.Handlers.Queries.OffLineController.OffLine;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Queries.OffLineController.OffLineAll
{
    public class OffLineAll : IRequest<OffLineResponse>
    {

        public class OffLineAllHandler : IRequestHandler<OffLineAll, OffLineResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public OffLineAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<OffLineResponse> Handle(OffLineAll request, CancellationToken cancellationToken)
            {
                OffLineResponse response = new OffLineResponse();
                var listMasterTable = await _unitOfWork.MasterTables.AsNoTracking().ToListAsync(cancellationToken);

                var listParent = await _unitOfWork.MasterTables.AsQueryable()
                                .Where(w => w.IdMasterTableParent == null)
                                .Distinct().AsNoTracking()
                                .ToListAsync(cancellationToken);

                response.ListMasterTable = _mapper.Map<List<ModelMasterTable>>(listMasterTable);
                response.ListMasterTableParent = _mapper.Map<List<ModelMasterTable>>(listParent);

                return response;
            }
        }

    }

}
