using BaseArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllParentByCount
{
    public class FindAllParentByCount : IRequest<List<FindAllParentByCountResponse>>
    {

        public class FindAllParentByCountHandler : IRequestHandler<FindAllParentByCount, List<FindAllParentByCountResponse>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public FindAllParentByCountHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }


            public async Task<List<FindAllParentByCountResponse>> Handle(FindAllParentByCount request, CancellationToken cancellationToken)
            {

                var nuevo = await (from a in _unitOfWork.MasterTables.AsQueryable()
                                   join b in _unitOfWork.MasterTables.AsQueryable()
                                       on a.IdMasterTableParent equals b.IdMasterTable into rightB
                                   from bb in rightB.DefaultIfEmpty()
                                   group a by new { a.IdMasterTableParent, bb.Name } into g
                                   select new FindAllParentByCountResponse
                                   {
                                       Count = g.Count(),
                                       Name = g.Key.Name
                                   })
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

                return nuevo;
            }
        }

    }

}
