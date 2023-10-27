namespace BaseArchitecture.ExternalServices.ServiceUniversal.Models
{
    public class UniversalEmployeeRequest
    {
        public string UserId { set; get; }
        public string Permission { set; get; }
        public string Filter { set; get; }
        public string Location { set; get; }
    }
}
