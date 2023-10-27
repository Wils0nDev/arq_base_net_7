
using BaseArchitecture.ExternalServices.Happy.Models;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public interface ISecurityServices
    {

        /// <summary>
        /// Petición de URL(POST): /api/Security/GetProfileByCode
        /// </summary>
        /// <param name="code"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<ApiGenericResponse<Response<string>>> GetProfileByCode(string code, string header);


        /// <summary>
        /// Petición de URL(POST): /api/Security/GetProfileSiapp
        /// </summary>
        /// <param name="code"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<ApiGenericResponse<Response<string>>> GetProfileSiapp(string code, string header);

        Task<ApiGenericResponse<Response<AWSCredentialsResponse>>> GetCredentialsByCode(string code, string header);

    }

}
