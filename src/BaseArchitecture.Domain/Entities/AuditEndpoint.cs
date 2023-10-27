namespace BaseArchitecture.Domain.Entities
{
    public class AuditEndpoint : BaseEntity, IGenerateEntity<AuditEndpoint>
    { 
        public int IdAuditEndpoint { get; set; }
        public int HttpStatusCode { get; set; }
        public int Retry { get; set; }
        public string Schema { get; set; }
        public string HostPort { get; set; }
        public string Path { get; set; }
        public string Method { get; set; } 
        public string RequestHeader { get; set; }
        public string RequestBody { get; set; }
        public string ResponseHeader { get; set; }
        public string ResponseBody { get; set; }
        public string TraceIdentifier { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? CreateDateOnly { get; set; } 
        public DateTime? CreateDate { get; set; }        

        public AuditEndpoint RecoverKey()
        {
            return new AuditEndpoint() { IdAuditEndpoint = this.IdAuditEndpoint };
        }
    }
}
