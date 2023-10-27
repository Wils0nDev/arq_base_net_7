using BaseArchitecture.Application.Handlers.Commands.MailController.SendMailBasic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MailController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("SendMailBasic")]
        public async Task<IActionResult> SendMailBasic(SendMailBasicRequest model)
        {
            var result = await _mediator.Send(new SendMailBasic(model));
            return Ok(result);
        }


    }
}
