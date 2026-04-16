using System;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Domain.Aggregate;

public sealed class PassengerReservation
{
    public PassengerReservationId Id { get; private set; } = null!;
    public FlightReservationId FlightReservationId { get; private set; } = null!;
    public PassengersId PassengerId { get; private set; } = null!;

    private PassengerReservation() { }

    public static PassengerReservation Create(Guid id, Guid flightReservationId, Guid passengerId)
    {
        return new PassengerReservation
        {
            Id = PassengerReservationId.Create(id),
            FlightReservationId = FlightReservationId.Create(flightReservationId),
            PassengerId = PassengersId.Create(passengerId)
        };
    }
}