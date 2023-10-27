using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.AwsS3.Models
{
    public class DownloadFileS3Response
    {
        public string key { get; set; }
        public string presigned { get; set; }
    }
}
