using BaseArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Infrastructure.Persistence.Configurations
{
    public class AuditEndpointConfiguration : IEntityTypeConfiguration<AuditEndpoint>
    { 
        public void Configure(EntityTypeBuilder<AuditEndpoint> builder)
        {
            builder.ToTable("AuditEndpoint", "Sgr");
            builder.HasKey(t => t.IdAuditEndpoint);
            builder.Property(t => t.IdAuditEndpoint).UseIdentityColumn();

            builder.Property(t => t.RequestHeader).HasColumnType("varchar(max)");
            builder.Property(t => t.RequestBody).HasColumnType("varchar(max)");
            builder.Property(t => t.ResponseBody).HasColumnType("varchar(max)");
            builder.Property(t => t.ResponseHeader).HasColumnType("varchar(max)");

            builder.Ignore(t => t.UserEditRecord);
            builder.Ignore(t => t.UserRecordCreation);
            builder.Ignore(t => t.RecordCreationDate);
            builder.Ignore(t => t.RecordEditDate);
            builder.Ignore(t => t.RecordStatus);
        }
    }
}
