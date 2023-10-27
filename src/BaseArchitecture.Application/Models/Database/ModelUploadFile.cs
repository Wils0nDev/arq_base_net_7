using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Models.Database
{
    public class ModelUploadFile
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public DateTime RegistryDate { get; set; }

        public ModelUploadFile RecoverKey()
        {
            return new ModelUploadFile() { Id = Id };
        }
    }
}
