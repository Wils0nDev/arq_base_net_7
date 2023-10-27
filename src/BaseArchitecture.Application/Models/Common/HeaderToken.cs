using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Models.Common
{
    public class HeaderToken
    {
        public string Token { get; set; }
        public string User { get; set; }
        public string UserEdit { get; set; }
        public string AwsSessionToken { get; set; }
        public string EmployeeId { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
        public string Email { get; set; }
        public string TimeExpiration { get; set; }
        public string ProfileId { get; set; }
        public string VicepresidencyId { get; set; }
        public string ManagementId { get; set; }
        public string TypeRisk { get; set; }
        public string AccessDevice { get; set; }
        public int? Duration { get; set; }
    }
}
