using GBMProject.Core.Entities;
using GBMProject.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMProject.Infrastructure.Persistence.Mappings;

public class DeliveryMapping : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Delivery");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.DeliveryDate)
            .HasColumnName("Date")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(d => d.Origin)
            .HasColumnName("Origin")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(d => d.Destiny)
            .HasColumnName("Destiny")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(d => d.CargoTransported)
            .HasColumnName("Cargo")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(d => d.DeliveryStatus)
            .HasColumnName("Status")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .HasConversion(v => v.ToString(),
                v => Enum.Parse<EDeliveryStatus>(v))
            .IsRequired();

        builder.HasOne(t => t.Truck)
            .WithMany()
            .HasForeignKey(t => t.TruckId)
            .HasConstraintName("TruckId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(d => d.Driver)
            .WithMany()
            .HasForeignKey(t => t.DriverId)
            .HasConstraintName("DriverId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasIndex(d => d.Id, "IX_Delivery_Id")
            .IsUnique();
    }
}