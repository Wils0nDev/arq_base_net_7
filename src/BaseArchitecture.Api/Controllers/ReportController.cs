using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllParentByCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reec.Inspection;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            this._mediator = mediator;
        }
         
        
        [HttpGet("MasterTable_FindAllParentByCount")]
        public async Task<IActionResult> MasterTable_FindAllParentByCount()
        {
            var result = await _mediator.Send(new FindAllParentByCount());
            if (result != null && result.Count == 0)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }

    }


}
