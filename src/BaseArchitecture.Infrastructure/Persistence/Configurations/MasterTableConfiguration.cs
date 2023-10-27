using BaseArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Infrastructure.Persistence.Configurations
{
    public class MasterTableConfiguration : IEntityTypeConfiguration<MasterTable>
    {
        public void Configure(EntityTypeBuilder<MasterTable> builder)
        {
            builder.ToTable("MasterTable", "Cnfg");
            builder.HasKey(t => t.IdMasterTable);
            
            builder.Property(t => t.IdMasterTableParent)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength(true);

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(t => t.Order);

            builder.Property(t => t.Value)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(t => t.AdditionalOne)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(t => t.AdditionalTwo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(t => t.AdditionalThree)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(t => t.RecordStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength(true)
                .IsRequired();

            builder.Property(t => t.UserRecordCreation)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(t => t.RecordCreationDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(t => t.UserEditRecord)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(t => t.RecordEditDate)
                .HasColumnType("datetime");
           
        }
    }
}
