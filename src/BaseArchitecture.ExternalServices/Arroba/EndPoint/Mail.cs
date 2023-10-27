using BaseArchitecture.ExternalServices.Mail.Base;
using BaseArchitecture.ExternalServices.Mail.Models;

namespace BaseArchitecture.ExternalServices.Mail.EndPoint
{
    public class Mail : IMail
    {
        private readonly IApiArroba _apiMail;

        public Mail(IApiArroba apiMail)
        {
            this._apiMail = apiMail;
        }

        public Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest)
        {
            return _apiMail.SendEmail(mailRequest);
        }
    }
}
