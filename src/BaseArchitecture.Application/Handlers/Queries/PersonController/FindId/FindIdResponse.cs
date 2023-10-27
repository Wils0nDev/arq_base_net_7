using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseArchitecture.Application.Models.Database;

namespace BaseArchitecture.Application.Handlers.Queries.PersonController.FindId
{
    public class FindIdResponse
    {
        public ModelPerson Person { get; set; }
        public List<ModelAttachedFile> ListAttachedFile { get; set; }

    }


}
