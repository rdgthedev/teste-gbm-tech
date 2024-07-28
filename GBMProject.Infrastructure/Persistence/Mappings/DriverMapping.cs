using GBMProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMProject.Infrastructure.Persistence.Mappings;

public class DriverMapping : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Driver");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(d => d.Cpf)
            .HasColumnName("Cpf")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(d => d.CnhCategory)
            .HasColumnName("CnhCategory")
            .HasColumnType("VARCHAR")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(d => d.BirthDate)
            .HasColumnName("BirthDate")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(d => d.Phone)
            .HasColumnName("Phone")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11)
            .IsRequired();

        builder.HasMany(d => d.Deliveries)
            .WithOne(d => d.Driver)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasIndex(d => d.Id, "IX_Driver_Id")
            .IsUnique();
        builder.HasIndex(d => d.Cpf, "IX_Driver_Cpf")
            .IsUnique();
    }
}