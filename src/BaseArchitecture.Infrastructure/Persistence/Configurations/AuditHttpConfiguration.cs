using BaseArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Infrastructure.Persistence.Configurations
{
    public class AuditHttpConfiguration : IEntityTypeConfiguration<AuditHttp>
    {
        public void Configure(EntityTypeBuilder<AuditHttp> builder)
        {
            builder.ToTable("AuditHttp", "Sgr");
            builder.HasKey(t => t.IdAuditHttp);
            builder.Property(t => t.IdAuditHttp).UseIdentityColumn();

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
