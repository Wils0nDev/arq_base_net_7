using BaseArchitecture.Application.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Handlers.Queries.OffLineController.OffLine
{
    public class OffLineResponse
    {
        public List<ModelMasterTable> ListMasterTable { get; set; }
        public List<ModelMasterTable> ListMasterTableParent { get; set; }

    }

}
