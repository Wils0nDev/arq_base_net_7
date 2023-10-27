using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Domain.Entities
{
    public class Person : BaseEntity, IGenerateEntity<Person>
    {

        public Guid IdPerson { get; set; }
        public string DNI { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Employment { get; set; } 
        public string PathFile { get; set; }

        public Person RecoverKey()
        {
            return new Person() { IdPerson = this.IdPerson };
        }

    }
}
