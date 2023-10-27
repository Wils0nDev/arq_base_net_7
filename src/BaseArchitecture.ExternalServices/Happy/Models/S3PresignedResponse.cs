using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.Models
{
    public class S3PresignedResponse
    {
        public string key { get; set; }
        public string presigned { get; set; }
    }
}
