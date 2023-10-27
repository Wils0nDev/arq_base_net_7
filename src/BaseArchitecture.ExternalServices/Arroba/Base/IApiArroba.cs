using BaseArchitecture.ExternalServices.Mail.Models;

namespace BaseArchitecture.ExternalServices.Mail.Base
{
    public interface IApiArroba
    {
        Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest);
    }
}
