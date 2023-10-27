using BaseArchitecture.ExternalServices.Happy.Base;
using BaseArchitecture.ExternalServices.Happy.Models;
using System.Reflection.PortableExecutable;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public class SecurityServices : ISecurityServices
    {
        private readonly IApiServiceHappy _apiServiceHappy;
        private readonly ServiceHappyOptions _serviceHappyOptions;

        public SecurityServices(IApiServiceHappy apiServiceHappy,
                                ServiceHappyOptions ServiceHappy)
        {
            this._apiServiceHappy = apiServiceHappy;
            this._serviceHappyOptions = ServiceHappy;
        }

        public async Task<ApiGenericResponse<Response<string>>> GetProfileByCode(string code, string header)
        {
            var url = this._serviceHappyOptions.Security_GetProfileByCode;
            return await _apiServiceHappy.PostAsync<Response<string>>(code, header, url, null);
        }

        public async Task<ApiGenericResponse<Response<string>>> GetProfileSiapp(string code, string header)
        {
            var url = this._serviceHappyOptions.Security_GetProfileSiapp;
            return await _apiServiceHappy.PostAsync<Response<string>>(code, header, url, null);
        }

        public async Task<ApiGenericResponse<Response<AWSCredentialsResponse>>> GetCredentialsByCode(string code, string header)
        {
            var url = this._serviceHappyOptions.Security_GetCredentialsByCode;
            return await _apiServiceHappy.GetAsync<Response<AWSCredentialsResponse>>(code, header, url);
        }
    }
}
