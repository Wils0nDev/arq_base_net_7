using BaseArchitecture.Application.Handlers.Commands.MasterTableController.Create;
using BaseArchitecture.Application.Handlers.Commands.MasterTableController.Delete;
using BaseArchitecture.Application.Handlers.Commands.MasterTableController.Update;
using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAll;
using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllAndDownloadExcel;
using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllByStoreProcedure;
using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllJsonExcel;
using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindAllParent;
using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindByIdParentAndName;
using BaseArchitecture.Application.Handlers.Queries.MasterTableController.FindId;
using BaseArchitecture.Application.Models.Database;
using BaseArchitecture.Common.Excel;
using BaseArchitecture.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Table;
using Reec.Helpers;
using Reec.Inspection;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    public class MasterTableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IGenerateExcel _generateExcel;
        private readonly ILogger<MasterTableController> _logger;
        public MasterTableController(IMediator mediator,
                                        IGenerateExcel generateExcel,
                                        ILogger<MasterTableController> logger)
        {
            this._mediator = mediator;
            this._generateExcel = generateExcel;
            _logger = logger;
        }

        [HttpGet("FindId/{idMasterTable}")]
        public async Task<IActionResult> FindId(string idMasterTable)
        {
            var result = await _mediator.Send(new FindId(idMasterTable));
            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }


        [HttpGet("FindAllParent")]
        public async Task<IActionResult> FindAllParent()
        {

            var result = await _mediator.Send(new FindAllParent());
            if (result == null)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros.");

            return Ok(result);
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> FindAll()
        {
            var result = await _mediator.Send(new FindAll());
            if (result != null && result.Count == 0)
                throw new ReecException(ReecEnums.Category.PartialContent, "No se encontraron registros");

            return Ok(result);
        }

        [HttpGet("FindAllAndDownloadExcel")]
        public async Task<IActionResult> FindAllAndDownloadExcel()
        {
            var file = await _mediator.Send(new FindAllAndDownloadExcel());

            return File(file.FileByte, file.ContentType, file.FileName);
        }


        [HttpGet("FindAllJsonExcel")]
        public async Task<IActionResult> FindAllJsonExcel()
        {

            var list = await _mediator.Send(new FindAllJsonExcel());

            return Ok(list);

        }


        [HttpGet("FindAllByStoreProcedureJsonExcel")]
        public async Task<IActionResult> FindAllByStoreProcedureJsonExcel()
        {
            var exportFile = await _mediator.Send(new FindAllByStoreProcedure());
            return Ok(exportFile);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(ModelMasterTable model)
        {
            var result = await _mediator.Send(new Create(model));
            return Ok(result);
        }

        [HttpPut("Update")]
        //[Produces("application/json")]
        public async Task<IActionResult> Update(ModelMasterTable model)
        {
            var result = await _mediator.Send(new Update(model));
            return Ok(result);
        }

        [HttpDelete("Delete/{idMasterTable}")]
        public async Task<IActionResult> Delete(string idMasterTable)
        {
            var result = await _mediator.Send(new Delete(idMasterTable));
            var message = new ReecMessage(ReecEnums.Category.OK, ConstantMessage.DeleteMessage);
            return Ok(message);
        }


        [HttpPost("FindByIdParentAndName")]
        public async Task<IActionResult> FindByIdParentAndName(FindByIdParentAndNameRequest request)
        {
            var result = await _mediator.Send(new FindByIdParentAndName(request));
            return Ok(result);
        }
        [HttpPost("Test")]
        public async Task<IActionResult> Test(FindByIdParentAndNameRequest request)
        {

            _logger.LogInformation("Ejemplo de logg information en Master table 1");
           
            _logger.LogError(new Exception("DemoPrueba 1"), "Ejemplo de error en dev 1");
            var uno = 1;
            var cero = 0;
            var a = uno / cero;
            return Ok(null);
        }

    }

}
