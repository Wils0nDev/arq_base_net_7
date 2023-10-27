using Serilog.Events;
using Serilog.Formatting;

namespace BaseArchitecture.Api.Model
{
    public class AWSTextFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            output.Write("level: {0} | Message: {1} {2} | Path : ", logEvent.Level, logEvent.MessageTemplate, output.NewLine);
            if (logEvent.Exception != null)
            {
                output.Write("Exception - {0}", logEvent.Exception);
            }
            var properties = logEvent.Properties;

            Serilog.Events.LogEventPropertyValue value;
            if (properties.TryGetValue("SourceContext", out value))
            {
                var formattedValueWorking = value.ToString().Replace("\"", "");

                output.Write(formattedValueWorking);
            }

        }
    }
}
