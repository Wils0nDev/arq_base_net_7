using BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListEmployee;
using BaseArchitecture.ExternalServices.Happy.EndPoint;
using BaseArchitecture.ExternalServices.Happy.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reec.Inspection;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesHappyController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public ServicesHappyController(IAuthentication authentication)
        {
            this._authentication = authentication;
        }

        [HttpGet("GetUserModel")]
        public async Task<IActionResult> GetUserModel([FromHeader] string jwt)
        {     
            var result = await this._authentication.GetUserModel(jwt);
            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }
    }
}
