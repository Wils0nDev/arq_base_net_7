using BaseArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Commands.DemoLogController.InternalServerError_SP_Transaction
{
    public class InternalServerError_SP_Transaction : IRequest<string>
    {

        public class InternalServerError_SP_TransactionHandler : IRequestHandler<InternalServerError_SP_Transaction, string>
        {
            private readonly IDemoLogRepository _demoLogRepository;

            public InternalServerError_SP_TransactionHandler(IDemoLogRepository demoLogRepository)
            {
                _demoLogRepository = demoLogRepository;
            }
            public async Task<string> Handle(InternalServerError_SP_Transaction request, CancellationToken cancellationToken)
            {

                return await _demoLogRepository.InternalServerError_SP_Transaction();

            }

        }

    }


}
