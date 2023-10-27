using BaseArchitecture.ExternalServices.Happy.Base;
using BaseArchitecture.ExternalServices.Happy.Models;
using Newtonsoft.Json;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public class Authentication : IAuthentication
    {
        private readonly IApiServiceHappy _apiServiceHappy;
        private readonly ServiceHappyOptions _serviceHappyOptions;

        public Authentication(IApiServiceHappy apiServiceHappy,
                                ServiceHappyOptions serviceHappyOptions)
        {
            this._apiServiceHappy = apiServiceHappy;
            this._serviceHappyOptions = serviceHappyOptions;
        }

        public async Task<ApiGenericResponse<Response<string>>> GetDeserializeObject(EncryptRequest entity)
        {
            var url = this._serviceHappyOptions.Authentication_GetDeserializeObject;
            return await _apiServiceHappy.PostAnonymousAsync<Response<string>>(url, entity);
        }
        public BaseRequest GetObjectTransform(string header)
        {
            return JsonConvert.DeserializeObject<BaseRequest>(header);
        }

        public async Task<ApiGenericResponse<Response<string>>> GetSerializeObject(EncryptRequest entity)
        {
            var url = this._serviceHappyOptions.Authentication_GetSerializeObject;
            return await _apiServiceHappy.PostAnonymousAsync<Response<string>>(url, entity);
        }

        public async Task<ApiGenericResponse<bool>> GetUserToken(LoginRequest entity)
        {
            var url = this._serviceHappyOptions.Authentication_GetUserToken;
            return await _apiServiceHappy.PostAnonymousAsync<bool>(url, entity);
        }

        public async Task<ApiGenericResponse<UserModelResponse>> GetUserModel(string entity)
        {
            var url = this._serviceHappyOptions.Authentication_GetUserModel;
            return await _apiServiceHappy.GetAnonymousAsync<UserModelResponse>(url, entity);
        }

    }
}
