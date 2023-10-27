using BaseArchitecture.Application.Handlers.Commands.PersonController.Create;
using BaseArchitecture.Application.Handlers.Commands.PersonController.CreateSP;
using BaseArchitecture.Application.Handlers.Commands.PersonController.Delete;
using BaseArchitecture.Application.Handlers.Commands.PersonController.Update;
using BaseArchitecture.Application.Handlers.Commands.PersonController.UpdateSP;
using BaseArchitecture.Application.Handlers.Queries.PersonController.FindAll;
using BaseArchitecture.Application.Handlers.Queries.PersonController.FindId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> FindAll()
        {
            var result = await _mediator.Send(new FindAll());
            return Ok(result);
        }

        [HttpGet("FindId/{idPerson}")]
        public async Task<IActionResult> FindId(Guid idPerson)
        {
            var result = await _mediator.Send(new FindId(idPerson, this.GetCode(), this.GetHeader()));
            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateRequest model)
        {
            var result = await _mediator.Send(new Create(model));
            return Ok(result);
        }

        [HttpPost("CreateSP")]
        public async Task<IActionResult> CreateSP(CreateSPRequest model)
        {
            var result = await _mediator.Send(new CreateSP(model));
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateRequest model)
        {
            var result = await _mediator.Send(new Update(model));
            return Ok(result);
        }

        [HttpPut("UpdateSP")]
        public async Task<IActionResult> UpdateSP(UpdateSPRequest model)
        {
            var result = await _mediator.Send(new UpdateSP(model));
            return Ok(result);
        }

        [HttpDelete("Delete/{idPerson}")]
        public async Task<IActionResult> Delete(Guid idPerson)
        {
            var result = await _mediator.Send(new Delete(idPerson));
            return Ok(result);
        }
    }

}
