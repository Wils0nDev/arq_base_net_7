using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy
{
    /// <summary>
    /// Configuración de uri base del api servicio programado y sus endpoint
    /// </summary>
    /// 
    public class ServiceHappyOptions
    {
        public string Uri { get; set; }
        public string Authentication_GetDeserializeObject { get; set; }
        public string Authentication_GetSerializeObject { get; set; }
        public string Authentication_GetUserToken { get; set; }
        public string Authentication_GetUserModel { get; set; }
        public string Security_GetProfileByCode { get; set; }
        public string Security_GetProfileSiapp { get; set; }
        public string Token_Create { get; set; }
        public string AwsUsername { get; set; }
        public string AwsPassword { get; set; }
        public string AwsIdentityPool { get; set; }
        public string AwsPoolId { get; set; }
        public string AwsClientId { get; set; }
        public string Aws_S3_Presigned { get; set;}
        public string Security_GetCredentialsByCode { get; set; }
    }
}
