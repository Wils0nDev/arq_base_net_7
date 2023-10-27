using BaseArchitecture.Common.Constant;
using BaseArchitecture.ExternalServices.Happy.Models;
using BaseArchitecture.ExternalServices.Mail.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Reec.Inspection;
using System.Net.Http.Headers;

namespace BaseArchitecture.ExternalServices.Mail.Base
{
    public class ApiArroba : IApiArroba
    {
        private readonly ServiceArrobaOptions _mailOptions;
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        public ApiArroba(ServiceArrobaOptions mailOptions, HttpClient httpClient, IServiceProvider serviceProvider)
        {
            this._mailOptions = mailOptions;
            this._httpClient = httpClient;
            this._serviceProvider = serviceProvider;
        }
        public async Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest)
        {
            ApiGenericResponse<MailResponse> apiResponse = new ApiGenericResponse<MailResponse>();
            var scope = _serviceProvider.CreateScope();
            var memory = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

            if (memory.TryGetValue(ConstantKey.Cognito_Token, out CognitoToken tokenCognito))
            {
                using var formData = new MultipartFormDataContent
                {
                    { new StringContent(mailRequest.AppOrigin), "AppOrigen" },
                    { new StringContent(mailRequest.MailFrom), "CorreoFrom" },
                    { new StringContent(mailRequest.MailFromAlias ?? ""), "CorreoFromAlias" },
                    { new StringContent(mailRequest.MailSubject), "CorreoSubject" },
                    { new StringContent(mailRequest.MailTo), "CorreoTo" },
                    { new StringContent(mailRequest.MailCc ?? ""), "CorreoCc" },
                    { new StringContent(mailRequest.MailBcc ?? ""), "CorreoBcc" },
                    { new StringContent(mailRequest.MailBodyHtml ?? ""), "CorreoBodyHtml" }
                };

                foreach (var file in mailRequest.FileAttached ?? Enumerable.Empty<KeyValuePair<string, byte[]>>())
                    formData.Add(new ByteArrayContent(file.Value), "FileAttached", file.Key);

                using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"),
                                                                        _mailOptions.MailQueue)
                { Content = formData };
                request.Headers.Authorization = new AuthenticationHeaderValue(tokenCognito.IdToken);

                using HttpResponseMessage response = await _httpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Result = JsonConvert.DeserializeObject<MailResponse>(jsonResponse);
                }
                else
                {
                    apiResponse.Result = JsonConvert.DeserializeObject<MailResponse>(jsonResponse);
                    apiResponse.Message = $"{response.ToString()} body: {jsonResponse}";
                }

            }
            else
                throw new ReecException(ReecEnums.Category.Warning,
                     "Ocurrio un error en obtener el Token de 'cognito de aws' o no tiene acceso al servicio.");

            return apiResponse;
        }

    }
}
