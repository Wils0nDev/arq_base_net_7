using BaseArchitecture.Domain.Enums;

namespace BaseArchitecture.Domain.Entities
{
    public class LogJob : BaseEntity, IGenerateEntity<LogJob>
    {
        public int IdLogJob { get; set; }
        public string NameJob { get; set; }
        public StateJob? StateJob { get; set; } 
        public string TraceIdentifier { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public string Data { get; set; }
        public DateTime? CreateDateOnly { get; set; }
        public DateTime? CreateDate { get; set; }

        public LogJob RecoverKey()
        {
            return new LogJob() { IdLogJob = IdLogJob };
        }
    }

}
