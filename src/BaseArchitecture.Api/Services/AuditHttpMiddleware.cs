using BaseArchitecture.Application.Interfaces.Repositories;
using BaseArchitecture.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
 
namespace BaseArchitecture.Api.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuditHttpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public AuditHttpMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            this._serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.HasValue &&
                !httpContext.Request.Path.Value.Contains("swagger") &&
                !httpContext.Request.Path.Value.Contains("index") &&
                !httpContext.Request.Path.Value.Contains("favicon"))
            {
                httpContext.Request.EnableBuffering();


              

                using var sr = new StreamReader(httpContext.Request.Body);
                var requestBody = await sr.ReadToEndAsync();

                httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

                var settings = new JsonSerializerSettings
                {
                    //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ContractResolver = new DefaultContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                Stopwatch stopwatch = Stopwatch.StartNew();
                var entity = new AuditHttp
                {
                    CreateDate = System.DateTime.Now,
                    HostPort = httpContext.Request.Host.Value,
                    Path = httpContext.Request.Path + httpContext.Request.QueryString,
                   // IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
                    Method = httpContext.Request.Method,
                    Schema = httpContext.Request.Scheme,
                    TraceIdentifier = httpContext.TraceIdentifier,
                    RequestBody = requestBody,
                    CreateDateOnly = DateTime.Now,
                    IpAddress = !string.IsNullOrEmpty( httpContext.Request.Headers.Origin.ToString())? httpContext.Request.Headers.Origin.ToString() : "Sin IP"
                };

                //Almacenar el flujo de cuerpo original para restaurar el cuerpo de respuesta a su flujo original.
                var originalBodyStream = httpContext.Response.Body;
                var responseBodyStream = new MemoryStream();
                httpContext.Response.Body = responseBodyStream;


                await _next(httpContext);

                // Restablecer la posición a 0 después de leer
                responseBodyStream.Seek(0, SeekOrigin.Begin);

                using var streamReader = new StreamReader(responseBodyStream);
                var responseBodyText = await streamReader.ReadToEndAsync();

                // Restablecer la posición a 0 después de leer
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                httpContext.Response.Body = originalBodyStream;
                await httpContext.Response.Body.WriteAsync(responseBodyStream.ToArray());



                //using var scope = _serviceProvider.CreateScope();
                //var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var headerRequest = httpContext.Request.Headers.Select(t => new { t.Key, Value = t.Value.ToString() });
                entity.RequestHeader = JsonConvert.SerializeObject(headerRequest, settings);

                var headerResponse = httpContext.Response.Headers.Select(t => new { t.Key, Value = t.Value.ToString() });
                entity.ResponseHeader = JsonConvert.SerializeObject(headerRequest, settings);
                entity.ResponseBody = responseBodyText;

                stopwatch.Stop();
                entity.Duration = stopwatch.Elapsed;
                entity.HttpStatusCode= httpContext.Response.StatusCode;

                var log = _serviceProvider.GetService<ILogger<AuditHttpMiddleware>>();
                var jsonlog = JsonConvert.SerializeObject(entity);
                log.LogInformation(jsonlog);


                //unitOfWork.AuditHttps.Create(entity);
                //await unitOfWork.SaveChangesAsync();
            }
            else
                await _next(httpContext);


        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuditHttpMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditHttpMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditHttpMiddleware>();
        }
    }
}
