using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Domain.Entities
{
    public class PersonFile : BaseEntity, IGenerateEntity<PersonFile>
    {
        public Guid IdPerson { get; set; }
        public Guid IdAttachedFile { get; set; }

        public PersonFile RecoverKey()
        {
            return new PersonFile() { IdAttachedFile = this.IdAttachedFile, IdPerson = this.IdPerson };
        }
    }

}
