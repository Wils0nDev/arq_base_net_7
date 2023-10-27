using BaseArchitecture.Application.Handlers.Commands.DemoBackgroundController.GenericProcess;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoBackgroundController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DemoBackgroundController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("GenericProcess")]
        public async Task<IActionResult> GenericProcess(GenericProcessRequest request)
        {
            var result = await _mediator.Send(new GenericProcess(request));
            return Ok(result); 
        }

    }

}
