using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.Models
{
    public class EncryptRequest
    {

        public string TextTransform { get; set; }
        public string KeyPassword { get; set; }
        public string KeyCryptographic { get; set; }
        public string KeyHexadecimal { get; set; }
        public string Code { get; set; }

    }
}
