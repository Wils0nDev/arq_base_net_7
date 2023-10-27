using AutoMapper;
using BaseArchitecture.Application.Models.Database;
using BaseArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Mappings
{
    public class MasterTableProfile : Profile
    {
        public MasterTableProfile()
        {
            CreateMap<ModelMasterTable, MasterTable>();
            CreateMap<MasterTable, ModelMasterTable>();
        }

    }

}
