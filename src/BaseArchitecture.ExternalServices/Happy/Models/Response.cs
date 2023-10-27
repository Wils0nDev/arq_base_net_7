using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.Models
{
    /// <summary>
    /// Objeto genérico de respuesta del api ServiceProgrammed
    /// </summary>
    /// <typeparam name="TypeObject">Tipo de objeto que se deserealiza en la propiedad "Value" del response</typeparam>
    public class Response<TypeObject>
    {
        public bool Status { get; set; } = true;
        public int State { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; } = "Ok";
        public TypeObject Value { set; get; }
        public ResultResponse ResultResponse { set; get; }
    }

}
