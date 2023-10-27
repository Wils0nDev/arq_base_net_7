namespace BaseArchitecture.ExternalServices.Happy.Base
{
    public interface IApiServiceHappy
    {
        /// <summary>
        /// Obtener de forma asíncrona los resultados de una peticion HttpGet enviando datos de cabecera.
        /// </summary>
        /// <typeparam name="TypeObject">Tipo de objeto que retorna el API en la propiedad Result</typeparam>
        /// <param name="code"></param>
        /// <param name="header"></param>
        /// <param name="url">Ejemplo: api/[controller]/[action]</param>
        /// <returns></returns>
        Task<ApiGenericResponse<TypeObject>> GetAsync<TypeObject>(string code, string header, string url, object entity = null) where TypeObject : class;


        /// <summary>
        /// Obtener de forma asíncrona los resultados de una peticion HttpPost enviando datos de cabecera.
        /// </summary>
        /// <typeparam name="TypeObject"></typeparam>
        /// <param name="code"></param>
        /// <param name="header"></param>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiGenericResponse<TypeObject>> PostAsync<TypeObject>(string code, string header, string url, object entity) where TypeObject : class;


        /// <summary>
        /// Obtener de forma asíncrona los resultados de una peticion HttpPost.
        /// </summary>
        /// <typeparam name="TypeObject"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiGenericResponse<TypeObject>> PostAnonymousAsync<TypeObject>(string url, object entity);

        Task<ApiGenericResponse<TypeObject>> GetAnonymousAsync<TypeObject>(string url, string entity) where TypeObject : class;

        Task<ApiGenericResponse<TypeObject>> PostChangeTokenAsync<TypeObject>() where TypeObject : class;

    }
}
