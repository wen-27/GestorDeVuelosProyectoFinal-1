using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Infrastructure.Entity;

public sealed class BoardingPassEntityConfiguration : IEntityTypeConfiguration<BoardingPassEntity>
{
    public void Configure(EntityTypeBuilder<BoardingPassEntity> builder)
    {
        builder.ToTable("boarding_passes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.CheckinId)
            .HasColumnName("checkin_id")
            .IsRequired();

        builder.Property(x => x.TicketId)
            .HasColumnName("ticket_id")
            .IsRequired();

        builder.Property(x => x.FlightId)
            .HasColumnName("flight_id")
            .IsRequired();

        builder.Property(x => x.Gate)
            .HasColumnName("gate")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.SeatCode)
            .HasColumnName("seat_code")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.BoardingAt)
            .HasColumnName("boarding_at")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.CheckinId).IsUnique();
        builder.HasIndex(x => x.TicketId).IsUnique();
        builder.HasIndex(x => x.FlightId);

        builder.HasOne(x => x.Checkin)
            .WithMany()
            .HasForeignKey(x => x.CheckinId)
            .HasConstraintName("fk_boarding_pass_checkin")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Ticket)
            .WithMany()
            .HasForeignKey(x => x.TicketId)
            .HasConstraintName("fk_boarding_pass_ticket")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Flight)
            .WithMany()
            .HasForeignKey(x => x.FlightId)
            .HasConstraintName("fk_boarding_pass_flight")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
