

namespace BaseArchitecture.ExternalServices.Mail.Models
{
    public class MailRequest
    {
        public string AppOrigin { get; set; }
        public string MailFrom { get; set; }
        public string MailFromAlias { get; set; }
        public string MailSubject { get; set; }
        public string MailTo { get; set; }
        public string MailCc { get; set; }
        public string MailBcc { get; set; }
        public string MailBodyHtml { get; set; }  
        public IEnumerable<KeyValuePair<string, byte[]>> FileAttached { get; set; }
        public string Now { get; set; }
    }
}
