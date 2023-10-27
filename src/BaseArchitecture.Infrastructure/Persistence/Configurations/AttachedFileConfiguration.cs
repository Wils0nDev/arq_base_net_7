using BaseArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Infrastructure.Persistence.Configurations
{
    public class AttachedFileConfiguration : IEntityTypeConfiguration<AttachedFile>
    { 
        public void Configure(EntityTypeBuilder<AttachedFile> builder)
        {
            builder.ToTable("AttachedFile", "Core");
            builder.HasKey(t => t.IdAttachedFile);

        }
    }


}
