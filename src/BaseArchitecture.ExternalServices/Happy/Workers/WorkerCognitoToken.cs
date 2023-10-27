using BaseArchitecture.Common.Constant;
using BaseArchitecture.ExternalServices.Happy.EndPoint;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BaseArchitecture.ExternalServices.Happy.Workers
{
    public class WorkerCognitoToken : BackgroundService
    {
        private readonly ILogger<WorkerCognitoToken> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IToken _token;

        public WorkerCognitoToken(ILogger<WorkerCognitoToken> logger, IMemoryCache memoryCache, IToken token)
        {
            this._logger = logger;
            this._memoryCache = memoryCache;
            this._token = token;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Solicitud de token Cognito: {time} - Inicio", DateTimeOffset.Now);

                var apiResponse = await _token.ChangeToken();
                if (apiResponse.IsSuccess)
                    _memoryCache.Set(ConstantKey.Cognito_Token, apiResponse.Result.Value, TimeSpan.FromMinutes(55));

                _logger.LogInformation("Solicitud de token Cognito: {time} - Fin", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromMinutes(50), stoppingToken);
            }
        }


    }
}