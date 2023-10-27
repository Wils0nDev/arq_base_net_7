using BaseArchitecture.Application.Handlers.Queries.OffLineController.OffLineAll;
using BaseArchitecture.Common.Helpers;
using BaseArchitecture.ExternalServices.Happy.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reec.Inspection;


namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffLineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        public OffLineController(IMediator mediator, IMemoryCache memoryCache)
        {
            this._mediator = mediator;
            this._memoryCache = memoryCache;
        }

        [HttpGet("OffLineAll")]
        public async Task<IActionResult> OffLineAll()
        {
            var result = await _mediator.Send(new OffLineAll());

            return Ok(result);
        }

        /// <summary>
        /// Se utiliza para validar si el token es válido y vigente para la sincronización de datos creados offline.
        /// </summary>
        /// <returns></returns>
        [HttpGet("PingToken")]
        public async Task<IActionResult> PingToken()
        {
            await Task.CompletedTask;
            var response = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.TokenValid);
            return Ok(response);
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            if (_memoryCache.TryGetValue(KeyNames.Cognito_Token, out CognitoToken tokenCognito))
            {
                return Ok(tokenCognito);
            }
            return Ok("Sin información");
        }

    }
}
