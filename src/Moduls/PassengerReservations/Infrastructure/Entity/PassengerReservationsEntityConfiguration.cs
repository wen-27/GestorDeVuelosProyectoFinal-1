// PassengerReservationsEntityConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Infrastructure.Entity;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Infrastructure.Entity;

public class PassengerReservationsEntityConfiguration : IEntityTypeConfiguration<PassengerReservationsEntity>
{
    public void Configure(EntityTypeBuilder<PassengerReservationsEntity> builder)
    {
        builder.ToTable("reservas_pasajeros");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Flight_Reservation_Id)
            .HasColumnName("reserva_vuelo_id")
            .IsRequired();

        builder.Property(x => x.Passenger_Id)
            .HasColumnName("pasajero_id")
            .IsRequired();

        builder.HasIndex(x => new { x.Flight_Reservation_Id, x.Passenger_Id })
            .IsUnique();

        builder.HasOne<FlightReservationsEntity>()
            .WithMany()
            .HasForeignKey(x => x.Flight_Reservation_Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<PassengersEntity>()
            .WithMany()
            .HasForeignKey(x => x.Passenger_Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}