using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.AwsS3
{
    public class AwsS3AntaminaOptions
    {

        public string AwsIdentityPool { get; set; } //= ConfigurationManager.AppSettings.Get("AWS_IDENTITY_POOL");
        public string AwsClientId { get; set; } //= ConfigurationManager.AppSettings.Get("AWS_CLIENT_ID");
        public string AwsUserPool { get; set; }  //= ConfigurationManager.AppSettings.Get("AWS_USER_POOL");
        public string AwsLogGroup { get; set; } // = ConfigurationManager.AppSettings.Get("AWS_LOG_GROUP");
        public string AwsRegion { get; set; } // = ConfigurationManager.AppSettings.Get("AWS_REGION");

        public string AwsBucketName { get; set; } //= ConfigurationManager.AppSettings.Get("AWS_BUCKET_NAME");
        public string AwsBucketFolder { get; set; }  //= ConfigurationManager.AppSettings.Get("AWS_BUCKET_FOLDER");

        public string AwsBucketComplete { get; set; }
        //$"{ConfigurationManager.AppSettings["AWS_BUCKET_NAME"]}/{ConfigurationManager.AppSettings["AWS_BUCKET_FOLDER"]}";
        public string AwsRegionEndPointS3 { get; set; }
        //ConfigurationManager.AppSettings.Get("AWS_REGION_END_POINT_S3");
        public int AwsDayExpire { get; set; }
        //Convert.ToInt32(ConfigurationManager.AppSettings.Get("AWS_DAYS_EXPIRE"));
        public string AwsIamAccessKey { get; set; }
        public string AwsIamSecretKey { get; set; }
    }

}
