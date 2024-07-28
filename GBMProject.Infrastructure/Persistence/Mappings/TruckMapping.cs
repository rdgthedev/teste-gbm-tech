using GBMProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBMProject.Infrastructure.Persistence.Mappings;

public class TruckMapping : IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.ToTable("Truck");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Plate)
            .HasColumnName("Plate")
            .HasColumnType("VARCHAR")
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(t => t.Model)
            .HasColumnName("Model")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.YearOfManifacture)
            .HasColumnName("YearOfManifacture")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(t => t.Color)
            .HasColumnName("Color")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.NumberOfAxles)
            .HasColumnName("NumberOfAxles")
            .HasColumnType("INT")
            .IsRequired();

        builder.HasMany(t => t.Deliveries)
            .WithOne(t => t.Truck)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasIndex(d => d.Id, "IX_Truck_Id");
        builder.HasIndex(d => d.Plate, "IX_Truck_Plate");
    }
}