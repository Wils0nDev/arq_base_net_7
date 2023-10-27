using BaseArchitecture.ExternalServices.Happy.Base;
using BaseArchitecture.ExternalServices.Happy.Models;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public class Token : IToken
    {
        private readonly IApiServiceHappy _apiServiceHappy;

        public Token(IApiServiceHappy apiServiceHappy)
        {
            this._apiServiceHappy = apiServiceHappy;
        }

        public async Task<ApiGenericResponse<ChangeTokenResponse>> ChangeToken()
        {
            return await _apiServiceHappy.PostChangeTokenAsync<ChangeTokenResponse>();
        }

    }

}
