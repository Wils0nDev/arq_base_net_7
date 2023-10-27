using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAll
{
    public class FindAll : IRequest<List<ModelMasterTable>>
    {

        public class FindAllHandler : IRequestHandler<FindAll, List<ModelMasterTable>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public FindAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<ModelMasterTable>> Handle(FindAll request, CancellationToken cancellationToken)
            {
                var entities = await _unitOfWork.MasterTables.AsNoTracking().ToListAsync(cancellationToken);
                var result = _mapper.Map<List<ModelMasterTable>>(entities);
                return result;
            }
        }

    }

}
