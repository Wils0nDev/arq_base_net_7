

namespace BaseArchitecture.ExternalServices.Mail.Models
{
    public class MailResponse
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string TraceIdentifier { get; set; }
        public int Category { get; set; }
        public string CategoryDescription { get; set; }
        public List<string> Message { get; set; }
    }
}
