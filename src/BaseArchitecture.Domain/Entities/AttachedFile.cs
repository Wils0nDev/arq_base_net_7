using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BaseArchitecture.Domain.Entities
{
    public class AttachedFile : BaseEntity, IGenerateEntity<AttachedFile>
    {
        public Guid IdAttachedFile { get; set; }
        public string Name { get; set; }
        public string PathFile { get; set; }

        public AttachedFile RecoverKey()
        {
            return new AttachedFile { IdAttachedFile = this.IdAttachedFile };
        }
    }

}
