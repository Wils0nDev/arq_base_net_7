using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Application.Models.Database;
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

namespace BaseArchitecture.Application.Handlers.Queries.PersonController.FindAll
{
    public class FindAll : IRequest<List<ModelPerson>>
    {
        public class FindAllHandler : IRequestHandler<FindAll, List<ModelPerson>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public FindAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }


            public async Task<List<ModelPerson>> Handle(FindAll request, CancellationToken cancellationToken)
            {
                var listEntity = await _unitOfWork.Persons.AsNoTracking().Where(x => x.RecordStatus == ConstantBase.Active).ToListAsync(cancellationToken);
                if (listEntity == null || listEntity.Count == 0)
                    throw new ReecException(ReecEnums.Category.PartialContent, ConstantMessage.NotRecords);

                var result = _mapper.Map<List<ModelPerson>>(listEntity);
                return result;
            }
        }
    }


}
