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
    public class PersonProfile: Profile
    {
        public PersonProfile()
        {
            CreateMap<ModelPerson, Person>();
            CreateMap<Person, ModelPerson>();
        }
    }

}
