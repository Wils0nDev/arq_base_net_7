using BaseArchitecture.ExternalServices.Happy.Models;

namespace BaseArchitecture.ExternalServices.Happy.EndPoint
{
    public interface IToken
    {
        Task<ApiGenericResponse<ChangeTokenResponse>> ChangeToken();
    }
}