using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.Models
{
    public class S3PresignedRequest
    {
        public string bucket { get; set; }
        public List<string> key { get; set; }
        public int? duration { get; set; }
    }
}
