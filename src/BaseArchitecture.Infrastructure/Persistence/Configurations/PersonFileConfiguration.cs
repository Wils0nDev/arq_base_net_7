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
    public class PersonFileConfiguration : IEntityTypeConfiguration<PersonFile>
    {
        public void Configure(EntityTypeBuilder<PersonFile> builder)
        {
            builder.ToTable("PersonFile", "Demo");
            builder.HasKey(t => new { t.IdPerson, t.IdAttachedFile });
        }
    }

}
