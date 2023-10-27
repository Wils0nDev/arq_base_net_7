using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoWorkflowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DemoWorkflowController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("MayorDeEdad")]
        public IActionResult MayorDeEdad()
        {


            return Ok();
        }

    }


}
