using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Infrastructure.Entities;

public sealed class StaffAvailabilityEntityConfiguration : IEntityTypeConfiguration<StaffAvailabilityEntity>
{
    public void Configure(EntityTypeBuilder<StaffAvailabilityEntity> builder)
    {
        builder.ToTable("staff_availability", table =>
        {
            table.HasCheckConstraint("chk_availability_dates", "ends_at > starts_at");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.StaffId)
            .HasColumnName("staff_id")
            .IsRequired();

        builder.Property(x => x.AvailabilityStatusId)
            .HasColumnName("availability_status_id")
            .IsRequired();

        builder.Property(x => x.StartsAt)
            .HasColumnName("starts_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.EndsAt)
            .HasColumnName("ends_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasColumnName("notes")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.HasOne<StaffEntity>()
            .WithMany()
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<AvailabilityStateEntity>()
            .WithMany()
            .HasForeignKey(x => x.AvailabilityStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Cuando exista el modulo Staff definitivo, reemplaza StaffEntity del modulo Personal
        // por la entidad de Staff y, si agregas navegacion, en Staff debes declarar algo como:
        // public ICollection<StaffAvailabilityEntity> Availabilities { get; set; } = new List<StaffAvailabilityEntity>();
    }
}