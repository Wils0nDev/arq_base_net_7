using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using AutoMapper;
using BaseArchitecture.Api.Model;
using BaseArchitecture.Api.Services;
using BaseArchitecture.Application;
using BaseArchitecture.Common;
using BaseArchitecture.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Reec.Inspection;
using Reec.Inspection.SqlServer;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.AwsCloudWatch;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder(args);
string outputTem = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz } {RequestId,13} [{Level:u3}] {Message:lj} {Properties} {NewLine}{Exception}";

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


 

Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .WriteTo.Console(outputTemplate: outputTem)
                 .ReadFrom.Configuration(configuration)
                 .CreateBootstrapLogger();


builder.Host
    .UseSerilog((context, services, configuration) =>
    {
        configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: outputTem);
       
       

        if (environment.IsEnvironment("Local"))
        {
            //  var awsCredentias = new InstanceProfileAWSCredentials();
            //var crendentials =   awsCredentias.GetCredentials();
            var a = "ASIA2ULOXBFOILBP7Q6S";
            var acc = "S/dgwQQExdUUjN/cP0eoidtSMpEC5rR65Yfg1uPG";
            var t = "IQoJb3JpZ2luX2VjEB4aCXVzLWVhc3QtMSJGMEQCIFebX/YwqWEzcgtLl9WefgOQVPkMAxuhZDxxn+cH569fAiAndflsroYrKPAa7XmCmMpE81EjIdQ5IKs1aCe5OVIwaSrEBAg3EAQaDDczMDkxMzUwNzY3NiIMFW2j/gm6g9KgK6dbKqEEz9alv43ajBUFXeurL/LQhzsriCv9K3pdK6O1CZmB+YVUZyZObmioQ6iTa5Z0gptbdBqvBMt8nRGLJC8g1w78DZhg5ptSsQr7mhKBVwHEM07q6bjRsSnNEhZiZEcAU/CL96NFfp0CgyxDZGSiZ7MgOJlWncjAcxr1sCZTnpS8DFzgYdwHiXxmdZW7qCb7bmsyrmQ5XA+IW/5Cf9Ao8OMmdB/b/nLYUjcqdEg7YEXxmRhP0CBzxj+TSSuKMQe+kQ2XPYS1EiqeuAXxku9hjgD9xEzr1duwqJ5OLR+9t2jjiBJyI64jGSxmdd5WkHLgIEJ/Gw3HhON5wP7kYax/yuJF67jzpr8pZ7GY08CKHAd0j/fP+Jy6qVFrHhpIdY5DKrE2InNJKL9a47HvuLYyLtOSyoZvCcO/BPVX8rh3MPWWXSInJCvLZL/xboDj36hXHv7H/09X8344NTXk49bkTNaq/QNCoDE//7ZmpZozqz8ehxTgWkl5io+8OR9/Sos2TJCJv/ulviZ1f55ihIc64TDIJSHcL+xPbTVMOjC/ufOJ1AkfTxmPg9qeggBYpmG3iKUqNLCGD9+2bUEj0PLFhaWd5SPOrSTSOsyYEHX88Y+wesu/33q3CDOAq0/d+Qp07KJS8vLZ5aBwyixKglZl2Lr3M7/vXnWexaGZoi/aiGJ66f5gbI0QF1cQ/docMqzlouLlN+IZ9RT86+6Fldfe0XhIRZowk/WmqQY6hgI2V0/IkyW6sPIjWMsohyqv7Op5s2ofsTzpDSVWNuqkpNVSH0TLxR7ZSVpScVBGtPqPdPPbhpctjE1hNiFg5M0rBUncwSnaNHJ+dDqNkEzoNBuljIHVvGtRUvQpF+C13Dw7RfvxLYhu2sbhKuwMMr6RSSfoRRZinTqfgfXIhqK7djeXYohOFvq0oKih098iO58ql8zazi8cQb63cyayg3/KeuZVu5YvPnVLpdXqd42xdwX5sEJXBk/27ZVSHYaBLgleYiOeXXtEJiU2n+Jfqh4VWeAIr57Qpk12KbYJHjPyg6ZFWXrZZ5FIWTGmsJ+O4qrHMAvVmSooUysu/aoNnA84xUnaoc8u";
            /*a,acc,t,*/
            var client = new AmazonCloudWatchLogsClient();
            var customFormatter = new AWSTextFormatter();
            var options = new CloudWatchSinkOptions
            {
                LogGroupName = "/aws/basearquitecture-3",
                TextFormatter = customFormatter,
                MinimumLogEventLevel = LogEventLevel.Information,
                BatchSizeLimit = 100,
                QueueSizeLimit = 10000,
                Period = TimeSpan.FromSeconds(10),
                CreateLogGroup = true,
                LogStreamNameProvider = new LogStreamProvider("ArchitectureBase.ApiInterna"),
                RetryAttempts = 5,
            };

            var log = configuration
            .Enrich.FromLogContext()
                      .WriteTo.AmazonCloudWatch(options,
                       cloudWatchClient: client);
                             
        }
        
    });

configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true);


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    })
    .AddXmlSerializerFormatters();

builder.Services.Configure<MvcOptions>(options =>
{
    var xmlWriterSettings = options.OutputFormatters
        .OfType<XmlSerializerOutputFormatter>()
        .Single().WriterSettings;
    xmlWriterSettings.OmitXmlDeclaration = false;
});

builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    c.CustomSchemaIds(type => type.FullName);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);
    c.DescribeAllParametersInCamelCase();
});

builder.Services.AddCommon();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration, environment.IsEnvironment("Local"));
builder.Services.AddInfrastructureExternalServices(configuration);

builder.Services.AddReecException<DbContextSqlServer>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DEV_STANDAR")),
        new ReecExceptionOptions
        {
            ApplicationName = "BaseArchitecture.ApiInterno",
            EnableMigrations = false,
            HeaderKeysExclude = new List<string> { "code", "header", "headertoken" },
            Schema = "Sgr"
        });

builder.Services.AddCors(o => o.AddPolicy("All", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

try
{
    Log.Information("Inicio de Api Interno...");
    var app = builder.Build();

    app.UsePathBase("/gateway");

    var supportedCultures = new[] { new CultureInfo("es-PE") };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("es-PE"),
        SupportedCultures = supportedCultures,
        FallBackToParentCultures = false
    });
    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("es-PE");

    app.UseAuditHttpMiddleware();
    app.UseReecException<DbContextSqlServer>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
        options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Api Base");
    });

    app.UseCors("All");
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    Log.Information("Deteniendo limpiamente la aplicaci�n Api Interno...");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Se produjo una excepci�n no controlada durante el arranque...");
}
finally
{
    Log.CloseAndFlush();
}