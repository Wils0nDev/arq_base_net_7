
namespace BaseArchitecture.ExternalServices
{
    /// <summary>
    /// Respuesta genérica para las respuesta de un api externo
    /// </summary>
    /// <typeparam name="TypeObject">Tipo de objeto tipado de respuesta del servicio.</typeparam>
    public class ApiGenericResponse<TypeObject> 
    {
        public bool IsSuccess { get; set; }
        public TypeObject Result { get; set; }
        public string Message { get; set; }
    }
}
