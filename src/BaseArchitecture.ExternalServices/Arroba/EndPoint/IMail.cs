using BaseArchitecture.ExternalServices.Mail.Models;

namespace BaseArchitecture.ExternalServices.Mail.EndPoint
{
    public interface IMail
    {
        Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest);
    }
}
