using AutoMapper;
using BaseArchitecture.Application.Handlers.Commands.PersonController.Update;
using BaseArchitecture.Application.Models.Database;
using BaseArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Mappings
{
    public class AttachedFileProfile: Profile
    {
        public AttachedFileProfile()
        {
            CreateMap<ModelAttachedFile, AttachedFile>();
            CreateMap<AttachedFile, ModelAttachedFile>();
             
            CreateMap<AttachedFile, AttachedFilesUpdate>();
            CreateMap<AttachedFilesUpdate, AttachedFile>();
        }
    }
}
