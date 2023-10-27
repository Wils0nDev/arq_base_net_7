using BaseArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using static BaseArchitecture.Application.Handlers.Commands.DemoLogController.InternalServerError_SP.InternalServerError_SP;

namespace BaseArchitecture.Application.Handlers.Commands.DemoLogController.InternalServerError_SP_Transaction_File
{ 
    public class InternalServerError_SP_Transaction_File : IRequest<string>
    {

        public class IInternalServerError_SP_Transaction_FileHandler : IRequestHandler<InternalServerError_SP_Transaction_File, string>
        {
            private readonly IDemoLogRepository _demoLogRepository;
            private readonly ILogger<IInternalServerError_SP_Transaction_FileHandler> _logger;

            public IInternalServerError_SP_Transaction_FileHandler(IDemoLogRepository demoLogRepository, 
                                                ILogger<IInternalServerError_SP_Transaction_FileHandler> logger)
            {
                _demoLogRepository = demoLogRepository;
                this._logger = logger;
            }
            public async Task<string> Handle(InternalServerError_SP_Transaction_File request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Ingreso a las validaciones de reglas de negocio");
                _logger.LogInformation("Culmino las reglas de negocio");
                return await _demoLogRepository.InternalServerError_SP_Transaction();

            }

        }

    }


}
