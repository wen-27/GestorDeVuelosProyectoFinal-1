using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Entity;

public sealed class AircraftEntityConfiguration : IEntityTypeConfiguration<AircraftEntity>
{
    public void Configure(EntityTypeBuilder<AircraftEntity> builder)
    {
        // Esta configuración deja explícito que la tabla real para la FK es "aircraft".
        builder.ToTable("aircraft");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.AircraftModelId)
            .HasColumnName("aircraft_model_id")
            .IsRequired();

        builder.Property(x => x.AirlinesId)
            .HasColumnName("airline_id")
            .IsRequired();

        builder.Property(x => x.Registration)
            .HasColumnName("registration")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.DateManufactured)
            .HasColumnName("date_manufactured")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
    }
}
