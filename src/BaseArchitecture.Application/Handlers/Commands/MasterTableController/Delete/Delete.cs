using AutoMapper;
using BaseArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Commands.MasterTableController.Delete
{
    public class Delete : IRequest<int>
    {
        public string IdMasterTable { get; }
        public Delete(string idMasterTable)
        {
            IdMasterTable = idMasterTable;
        }

        public class DeleteHandler : IRequestHandler<Delete, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(Delete request, CancellationToken cancellationToken)
            {
                await _unitOfWork.MasterTables.DeleteAsync(request.IdMasterTable);
                return await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

        }


    }
}
