using BaseArchitecture.Application.Handlers.Commands.DemoLogController.InternalServerError_SP;
using BaseArchitecture.Application.Handlers.Commands.DemoLogController.InternalServerError_SP_Transaction;
using BaseArchitecture.Application.Handlers.Commands.DemoLogController.InternalServerError_SP_Transaction_File;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reec.Inspection;
using static Reec.Inspection.ReecEnums;

namespace BaseArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoLogController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DemoLogController> _logger;

        public DemoLogController(IMediator mediator, ILogger<DemoLogController> logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }


        /// <summary>
        /// Se utiliza para validar si los datos guardados cumplen con los requisitos.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ReecException"></exception>
        [HttpGet("Warning/{parameter}")]
        public IActionResult Warning(string parameter)
        {
            // Error controlado de validación de datos
            if (!string.IsNullOrWhiteSpace(parameter) && parameter.ToLower() == "test")
                throw new ReecException(Category.Warning, "Campo 'parameter' obligatorio.");

            return Ok(parameter);
        }

        /// <summary>
        /// Se utiliza cuando exísta ERRORES CONTROLADOS por el sistema.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ReecException"></exception>
        [HttpGet("BusinessLogic/{parameter}")]
        public IActionResult BusinessLogic(string parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter) && parameter.ToLower() == "test")
                throw new ReecException(Category.BusinessLogic, "No cumple con la regla de negocio.");
            return Ok(parameter);
        }

        /// <summary>
        /// Se utiliza cuando existe ERRORES CONTROLADOS de un SISTEMA EXISTENTE(Base de Datos, Servicio Api, etc).
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ReecException"></exception>
        [HttpGet("BusinessLogicLegacy/{parameter}")]
        public IActionResult BusinessLogicLegacy(string parameter)
        {

            //Simulación de una regla de negocio legacy de conexion a un api o Base de datos
            if (!string.IsNullOrWhiteSpace(parameter) && parameter.ToLower() == "test")
                throw new ReecException(Category.BusinessLogicLegacy, "Error controlado del sistema legacy 'app1'.");

            return Ok(parameter);

        }

        /// <summary>
        /// Se utiliza cuando exíste ERRORES NO CONTROLADOS por el sistema
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpGet("InternalServerError/{parameter}")]
        public IActionResult InternalServerError(string parameter)
        {
            var numerador = 1;
            var denominador = 0;
            var division = numerador / denominador;
            return Ok(parameter);
        }

        /// <summary>
        /// Se utiliza cuando exíste ERRORES NO CONTROLADOS de un SISTEMA EXISTENTE.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ReecException"></exception>
        [HttpGet("BadGateway/{parameter}")]
        public IActionResult BadGateway(string parameter)
        {
            try
            {
                //Simular que se conectan a un Api Externo y cayo en error no controlado.
                var numerador = 1;
                var denominador = 0;
                var division = numerador / denominador;
                return Ok(parameter);
            }
            catch (Exception ex)
            {
                throw new ReecException(Category.BadGateway, "Error no controlado del sistema legacy 'app1'.", ex.Message);
            }
        }

        /// <summary>
        /// Se utiliza cuando se conecta a un SISTEMA EXISTENTE y supera el TIEMPO DE ESPERA.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ReecException"></exception>
        [HttpGet("GatewayTimeout/{parameter}")]
        public IActionResult GatewayTimeout(string parameter)
        {
            try
            {
                //Simular timeout que pueda ocurrir al conectarse a un api o sistema existente.
                var numerador = 1;
                var denominador = 0;
                var division = numerador / denominador;
                return Ok(parameter);
            }
            catch (Exception ex)
            {
                throw new ReecException(Category.GatewayTimeout, "Tiempo de espera agotado al conectarse al servicio 'App1'", ex.Message);
            }
        }


        /// <summary>
        /// Capturar error no controlado desde StoreProcedure.
        /// </summary>
        /// <returns></returns>
        [HttpGet("InternalServerError_SP")]
        public async Task<IActionResult> InternalServerError_SP()
        {           
            var result = await _mediator.Send(new InternalServerError_SP());
            return Ok(result);
        }



        /// <summary>
        /// Capturar error no controlado desde StoreProcedure que contiene una Transacción.
        /// </summary> 
        /// <returns></returns>
        [HttpGet("InternalServerError_SP_Transaction")]
        public async Task<IActionResult> InternalServerError_SP_Transaction()
        {
            var result = await _mediator.Send(new InternalServerError_SP_Transaction());
            return Ok(result);
        }

         

        /// <summary>
        /// Capturar error no controlado desde StoreProcedure que contiene una Transacción y se guarda en un archivo.
        /// </summary> 
        /// <returns></returns>
        [HttpGet("InternalServerError_SP_Transaction_File")]
        public async Task<IActionResult> InternalServerError_SP_Transaction_File()
        {
            _logger.LogInformation("Ingreso a validaciones");
            var result = await _mediator.Send(new InternalServerError_SP_Transaction_File());
            return Ok(result);
        }


    }


}
