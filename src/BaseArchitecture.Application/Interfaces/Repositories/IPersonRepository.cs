using BaseArchitecture.Application.Models.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<int> CreateAndUpdateByStoredProcedure(ModelPerson person);
    }
}
