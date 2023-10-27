using BaseArchitecture.Domain.Entities;
using BaseArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Infrastructure.Persistence.Configurations
{
    public class LogJobConfiguration : IEntityTypeConfiguration<LogJob>
    {
        public void Configure(EntityTypeBuilder<LogJob> builder)
        {
            builder.ToTable("LogJob", "Sgr");
            builder.HasKey(t => t.IdLogJob);
            builder.Property(t => t.IdLogJob)
                .UseIdentityColumn();
            
            builder.Property(t => t.StateJob)
                .HasConversion(
                    v => v.ToString(),
                    v => (StateJob)Enum.Parse(typeof(StateJob), v));

            builder.Ignore(t => t.UserEditRecord);
            builder.Ignore(t => t.UserRecordCreation);
            builder.Ignore(t => t.RecordCreationDate);
            builder.Ignore(t => t.RecordEditDate);
            builder.Ignore(t => t.RecordStatus);
        }
    }
}
