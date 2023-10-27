using BaseArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Infrastructure.Persistence.Configurations
{
    public class LogDbConfiguration : IEntityTypeConfiguration<LogDb>
    {
        public void Configure(EntityTypeBuilder<LogDb> builder)
        { 
            builder.ToTable("LogDb", "Sgr");
            builder.HasKey(t => t.IdLogDb);
            builder.Property(t => t.IdLogDb).UseIdentityColumn();


            builder.Ignore(t => t.UserEditRecord);
            builder.Ignore(t => t.UserRecordCreation);
            builder.Ignore(t => t.RecordCreationDate);
            builder.Ignore(t => t.RecordEditDate);
            builder.Ignore(t => t.RecordStatus);
        }
    }
}
