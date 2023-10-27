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

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindByIdParentAndName
{
    public class FindByIdParentAndName : IRequest<List<ModelMasterTable>>
    {
        public FindByIdParentAndNameRequest FindByIdParenAndNameRequest { get; }

        public FindByIdParentAndName(FindByIdParentAndNameRequest findByIdParenAndNameRequest)
        {
            FindByIdParenAndNameRequest = findByIdParenAndNameRequest;
        }



        public class QueryFindIdHandler : IRequestHandler<FindByIdParentAndName, List<ModelMasterTable>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public QueryFindIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<ModelMasterTable>> Handle(FindByIdParentAndName request, CancellationToken cancellationToken)
            {
                string idMasterTableParent = null;
                if (!string.IsNullOrWhiteSpace(request.FindByIdParenAndNameRequest.IdMasterTableParent))
                    idMasterTableParent = request.FindByIdParenAndNameRequest.IdMasterTableParent.Trim();

                string name = null;
                if (!string.IsNullOrWhiteSpace(request.FindByIdParenAndNameRequest.Name))
                    name = request.FindByIdParenAndNameRequest.Name.Trim();

                var list = await _unitOfWork.MasterTables.AsQueryable()
                                .Where(w =>
                                    (w.IdMasterTableParent == idMasterTableParent || idMasterTableParent == null) &&
                                    (w.Name.Contains(name) || name == null))
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

                if (list != null)
                    return _mapper.Map<List<ModelMasterTable>>(list);

                return null;
            }
        }


    }
}
