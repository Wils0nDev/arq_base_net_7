using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Interfaces.Repositories
{
    public interface IDemoLogRepository
    {
        Task<string> InternalServerError_SP();

        Task<string> InternalServerError_SP_Transaction(); 

    }
}
