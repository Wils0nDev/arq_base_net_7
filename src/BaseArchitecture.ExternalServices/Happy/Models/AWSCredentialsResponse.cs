using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.Models
{
    public class AWSCredentialsResponse
    {
        public AwsCredentialsByCode aws { get; set; }
    }

    public class AwsCredentialsByCode
    {
        public string awsSessionToken { get; set; }
        public string awsSecretKey { get; set; }
        public string awsAccessKey { get; set; }
      
    }
}
