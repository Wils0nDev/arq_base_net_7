using BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListCompany;
using BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListEmployee;
using BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListManagement;
using BaseArchitecture.Application.Handlers.Queries.ServiceUniversal.ListPosition;
using BaseArchitecture.ExternalServices.ServiceUniversal.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reec.Inspection;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceUniversalController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceUniversalController(IMediator mediator)
        {
            this._mediator = mediator; 
        }

        [HttpPost("ListEmployee")]
        public async Task<IActionResult> ListEmployee()
        {
            var result = await _mediator.Send(new ListEmployee(this.GetCode(), this.GetHeader()));
            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }

        [HttpPost("ListCompany")]
        public async Task<IActionResult> ListCompany(UniversalCompanyRequest request)
        {
            var result = await _mediator.Send(new ListCompany(request));
            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }

        [HttpPost("ListPosition")]
        public async Task<IActionResult> ListPosition(UniversalPositionRequest request)
        {
            var result = await _mediator.Send(new ListPosition(request));
            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }

        [HttpPost("ListManagement")]
        public async Task<IActionResult> ListManagement(UniversalManagementRequest request)
        {
            var result = await _mediator.Send(new ListManagement(request));
            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }
    }
}
