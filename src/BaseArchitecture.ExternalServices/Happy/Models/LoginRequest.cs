using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.Models
{
     public class LoginRequest
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
        public string Device { get; set; }
        public string PreferredUser { get; set; }
        public string AwsIdentityPool { get; set; }
        public bool HaveAccessS3 { get; set; }
        public string AwsUserPool { get; set; }
    }

}
