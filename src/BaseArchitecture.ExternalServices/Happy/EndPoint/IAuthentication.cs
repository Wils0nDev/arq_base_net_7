using BaseArchitecture.ExternalServices.Happy.Models;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public interface IAuthentication
    {
        /// <summary>
        /// Petición Post(URL: /api/Authentication/GetDeserializeObject)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiGenericResponse<Response<string>>> GetDeserializeObject(EncryptRequest entity);

        /// <summary>
        /// Petición Post(URL: /api/Authentication/GetSerializeObject
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiGenericResponse<Response<string>>> GetSerializeObject(EncryptRequest entity);

        /// <summary>
        /// Petición Post(URL: /api/Authentication/GetUserToken
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiGenericResponse<bool>> GetUserToken(LoginRequest entity);

        BaseRequest GetObjectTransform(string header);

        Task<ApiGenericResponse<UserModelResponse>> GetUserModel(string entity);

    }
}