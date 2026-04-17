using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Entity;

public sealed class AircraftModelsEntityConfiguration : IEntityTypeConfiguration<AircraftModelsEntity>
{
    public void Configure(EntityTypeBuilder<AircraftModelsEntity> builder)
    {
        builder.ToTable("aircraft_manufacturers");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(m => m.AircraftManufacturerId)
            .HasColumnName("manufacturer_id")
            .IsRequired();  // fk

        builder.Property(m => m.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Capacity)
            .HasColumnName("capacity")
            .IsRequired();

        builder.Property(m => m.Weight)
            .HasColumnName("weight")
            .HasColumnType("decimal(18,2)");

        builder.Property(m => m.FuelConsumption)
            .HasColumnName("fuel_consumption")
            .HasColumnType("decimal(18,2)");

        builder.Property(m => m.CruiseSpeed)
            .HasColumnName("cruise_speed")
            .HasColumnType("int");

        builder.Property(m => m.CruiseAltitude)
            .HasColumnName("cruise_altitude")
            .HasColumnType("int");
    }
}
