using BaseArchitecture.Application.Handlers.Commands.AttachedFileController.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachedFileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AttachedFileController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpDelete("Delete/{idAttachedFile}")]
        public async Task<IActionResult> Delete(Guid idAttachedFile)
        {
            var result = await _mediator.Send(new Delete(idAttachedFile));
            return Ok(result);
        }
    }
}
