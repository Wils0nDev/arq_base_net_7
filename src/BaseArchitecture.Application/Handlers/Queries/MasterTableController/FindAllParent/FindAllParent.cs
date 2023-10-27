using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Common;
using BaseArchitecture.Application.Models.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllParent
{
    public class FindAllParent : IRequest<List<ModelMasterTable>>
    {

        public class FindByIdParentIdHandler : IRequestHandler<FindAllParent, List<ModelMasterTable>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public FindByIdParentIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<ModelMasterTable>> Handle(FindAllParent request, CancellationToken cancellationToken)
            {

                var listEntity = await _unitOfWork.MasterTables.AsQueryable()
                                .Where(w => w.IdMasterTableParent == null)
                                .Distinct().AsNoTracking()
                                .ToListAsync(cancellationToken);

                var result = _mapper.Map<List<ModelMasterTable>>(listEntity);
                return result;

            }
        }

    }
}
