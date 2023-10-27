using Serilog.Sinks.AwsCloudWatch;
using System.Net;

namespace BaseArchitecture.Api.Model
{
    public class LogStreamProvider : ILogStreamNameProvider
    {
        private readonly string DATETIME_FORMAT = "yyyy-MM-dd";
        public string Nombre { get; set; }
        public LogStreamProvider(string _nombre)
        {
            Nombre = _nombre;
        }
        public string GetLogStreamName()
        {
            return $"{Nombre}_{DateTime.UtcNow.ToString(DATETIME_FORMAT)}";
        }
    }
}
