using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.ExternalServices.Happy.Models
{
    public class ResultResponse
    {
        public int Id { get; set; }
        public int IdResult { get; set; }
        public int CodeResult { get; set; }
        public string MessageResult { get; set; }
    }

}
