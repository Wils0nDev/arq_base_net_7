using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Http;
using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Domain.Entities;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Utilities;

namespace BaseArchitecture.Infrastructure
{
    public static class CustomPolly
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IServiceProvider serviceProvider, HttpRequestMessage httpRequestMessage)
        {
            var count = 3;
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                //.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .OrResult(response =>
                {
                    var log = serviceProvider.GetService<ILogger<MasterTable>>();
                    bool flgRegister = false;
                    var headerRetry = response.RequestMessage.Headers.FirstOrDefault(t => t.Key == "retry");
                    var numRetry = headerRetry.Value == null ? 0 : int.Parse(headerRetry.Value.First());

                    if (response.IsSuccessStatusCode) flgRegister = true;
                    else if (!response.IsSuccessStatusCode && headerRetry.Key != null) if (numRetry == count) flgRegister = true;

                    if (flgRegister)
                    {
                        var settings = new JsonSerializerSettings
                        {
                            ContractResolver = new DefaultContractResolver(),
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        };
                        var tempRequestHeader = response.RequestMessage.Headers.Select(t => new { t.Key, Value = t.Value.Any() ? string.Join(",", t.Value.ToList()) : t.Value.ToString() });
                        
                        var requestHeader = JsonConvert.SerializeObject(tempRequestHeader, settings);

                        var tempResponseHeader = response.Headers.Select(t => new { t.Key, Value = t.Value.Any() ? string.Join(",", t.Value.ToList()) : t.Value.ToString() });
                        var responseHeader = JsonConvert.SerializeObject(tempResponseHeader, settings);

                        var trace = serviceProvider.GetService<IHttpContextAccessor>();
                        var traceIdentifier = trace.HttpContext is null ? null : trace.HttpContext.TraceIdentifier;


                        string requestBody = null;
                        if (response.RequestMessage.Content != null)                        
                            requestBody = response.RequestMessage.Content.ReadAsStringAsync().Result;

                        string responseBody = null;
                        if (response.Content != null)
                            responseBody = response.Content.ReadAsStringAsync().Result;
                        
                        
                        var auditHttpDetailRequest = new AuditEndpoint
                        {
                            CreateDate = System.DateTime.Now,
                            CreateDateOnly = System.DateTime.Now,
                            Schema = response.RequestMessage.RequestUri.Scheme,
                            HostPort = string.Format("{0}:{1}", response.RequestMessage.RequestUri.Host, response.RequestMessage.RequestUri.Port),
                            Path = response.RequestMessage.RequestUri.PathAndQuery,
                            Method = response.RequestMessage.Method.Method,
                            TraceIdentifier = traceIdentifier,
                            Retry = numRetry,
                            RequestHeader = requestHeader,
                            RequestBody = requestBody,
                            ResponseHeader = responseHeader,
                            ResponseBody = responseBody,
                            HttpStatusCode = (int)response.StatusCode,                              
                        };
                        //TODO
                        var jsonlog = JsonConvert.SerializeObject(auditHttpDetailRequest);
                        log.LogInformation(jsonlog);
                       // _ = RegisterAuditEndpoint(serviceProvider, auditHttpDetailRequest);
                    }

                    return !response.IsSuccessStatusCode;
                })
                .WaitAndRetryAsync(
                    retryCount: count,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {                        
                        httpRequestMessage.Headers.Remove("retry");
                        httpRequestMessage.Headers.Add("retry", retryCount.ToString());
                        //Aquí se puede agregar logica antes de renviar el request al endpoint
                        if (retryCount == count)
                        {

                        }
                    });

            //var policy = Policy.WrapAsync(retry);
            //Este metodo nos sirve para interceptar la petición request antes de ser enviado.
            //policy.ExecuteAndCaptureAsync()

            return retry;

        }

        private static async Task RegisterAuditEndpoint(IServiceProvider serviceProvider, AuditEndpoint request)
        {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            unitOfWork.AuditEndpoints.Create(request);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
