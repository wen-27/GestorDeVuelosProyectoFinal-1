using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Infrastructure.Persistence.Entities;

public sealed class StaffAvailabilityStateReferenceEntityConfiguration : IEntityTypeConfiguration<StaffAvailabilityStateReferenceEntity>
{
    public void Configure(EntityTypeBuilder<StaffAvailabilityStateReferenceEntity> builder)
    {
        builder.ToTable("staff_availability");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.StaffId)
            .HasColumnName("staff_id");

        builder.Property(x => x.AvailabilityStateId)
            .HasColumnName("availability_status_id");
    }
}
