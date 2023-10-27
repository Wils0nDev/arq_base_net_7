using BaseArchitecture.Application.Models.Database;
using BaseArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseArchitecture.Common.Helpers.BaseEnums;

namespace BaseArchitecture.Application.Handlers.Commands.PersonController.Update
{

    public class UpdateRequest
    {
        public ModelPerson Person { get; set; }
        public List<AttachedFilesUpdate> ListAttachedFile { get; set; }

    }

    public class AttachedFilesUpdate
    {
        public Guid IdAttachedFile { get; set; }
        public string Name { get; set; }
        public string PathFile { get; set; }
        //public string UserRecordCreation { get; set; }
        //public DateTime RecordCreationDate { get; set; }
        //public string UserEditRecord { get; set; }
        //public DateTime? RecordEditDate { get; set; }
        public string RecordStatus { get; set; }
        public StatusFile StatusFile { get; set; }
    }


}
