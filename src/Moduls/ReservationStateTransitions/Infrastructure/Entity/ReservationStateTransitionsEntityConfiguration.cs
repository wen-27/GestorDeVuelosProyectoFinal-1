using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.src.Moduls.ReservationStateTransitions.Infrastructure.Entity;

public class ReservationStateTransitionsEntityConfiguration : IEntityTypeConfiguration<ReservationStateTransitionsEntity>
{
    public void Configure(EntityTypeBuilder<ReservationStateTransitionsEntity> builder)
    {
        builder.ToTable("reservation_state_transitions", t =>
        {
            t.HasCheckConstraint(
                "CK_ReservationStatusTransitions_NoSelf",
                "from_status_id <> to_status_id"
            );
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.From_Status_Id)
            .HasColumnName("from_status_id")
            .IsRequired();

        builder.Property(x => x.To_Status_Id)
            .HasColumnName("to_status_id")
            .IsRequired();

        builder.HasIndex(x => new { x.From_Status_Id, x.To_Status_Id })
            .IsUnique();

        builder.HasOne(x => x.FromStatus)
            .WithMany()
            .HasForeignKey(x => x.From_Status_Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToStatus)
            .WithMany()
            .HasForeignKey(x => x.To_Status_Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
