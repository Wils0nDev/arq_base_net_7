using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerBaseExtensions
    {

        /// <summary>
        /// Obtener el parametro 'code' de la cabecera del request.
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <returns></returns>
        public static string GetCode(this ControllerBase controllerBase)
        {

            var result = controllerBase.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "code");
            if (!string.IsNullOrWhiteSpace(result.Value))
                return result.Value;
            else
                throw new ArgumentNullException("Code", "Debe enviar el parametro 'Code' en la cabecera del request.");

        }


        /// <summary>
        /// Obtener el parametro 'header' de la cabecera del request.
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <returns></returns>
        public static string GetHeader(this ControllerBase controllerBase)
        {

            var result = controllerBase.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "header");
            if (!string.IsNullOrWhiteSpace(result.Value))
                return result.Value;
            else
                throw new ArgumentNullException("Header", $"Debe enviar el parametro 'Header' en la cabecera del request.");

        }


    }
}
