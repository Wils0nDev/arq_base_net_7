using BaseArchitecture.ExternalServices.Mail.EndPoint;
using BaseArchitecture.ExternalServices.Mail.Models;
using BaseArchitecture.ExternalServices.Happy.EndPoint;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BaseArchitecture.Application.Handlers.Commands.MailController.SendMailBasic
{ 
    public class SendMailBasic : IRequest<MailResponse>
    {
        public SendMailBasicRequest Model { get; }

        public SendMailBasic(SendMailBasicRequest model)
        {
            Model = model;
        }

        public class SendMailBasicHandler : IRequestHandler<SendMailBasic, MailResponse>
        {
            private readonly IMail _mail;
            private readonly IAuthentication _authentication;
            private readonly ILogger<SendMailBasicHandler> _logger;

            public SendMailBasicHandler(IMail mail, IAuthentication authentication, ILogger<SendMailBasicHandler> logger)
            {
                _mail = mail;
                _authentication = authentication;
                this._logger = logger;
            }

            public async Task<MailResponse> Handle(SendMailBasic request, CancellationToken cancellationToken)
            {

                var sendMailRequest = new MailRequest()
                {
                    AppOrigin = "BASEARCHITECTURE",
                    MailFrom = "noreply@antamina.com",
                    MailFromAlias = request.Model.MailFromAlias ?? string.Empty,
                    MailSubject = request.Model.MailSubject ?? "BASEARCHITECTURE",
                    MailTo = request.Model.MailTo,
                    MailBodyHtml = request.Model.MailBodyHtml
                };

                var response = await _mail.SendEmail(sendMailRequest);
                return response.Result;

            }
        }
    }
}
