using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Infrastructure.Entity;

public class PassengerReservationsEntity
{
    public int Id { get; set; }

    public int Flight_Reservation_Id { get; set; }

    public int Passenger_Id { get; set; }
}