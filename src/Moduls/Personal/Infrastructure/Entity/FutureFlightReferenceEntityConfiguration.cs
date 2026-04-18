using GestorDeVuelosProyectoFinal.Moduls.Personal.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.Moduls.Personal.Infrastructure.Persistence.Entities;

public sealed class FutureFlightReferenceEntityConfiguration : IEntityTypeConfiguration<FutureFlightReferenceEntity>
{
    public void Configure(EntityTypeBuilder<FutureFlightReferenceEntity> builder)
    {
        builder.ToTable("flights");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.AircraftId)
            .HasColumnName("aircraft_id")
            .IsRequired(false);

        builder.Property(x => x.DepartureTime)
            .HasColumnName("departure_time");
    }
}
