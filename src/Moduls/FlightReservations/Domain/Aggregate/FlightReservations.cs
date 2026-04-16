using System;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;
using FlightsValueObject = GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.Aggregate;

// Cambiamos el nombre de la clase a singular para evitar conflicto con el namespace
public sealed class FlightReservation
{
    public FlightReservationId Id { get; private set; } = null!;
    public ReverseId ReservationId { get; private set; } = null!;
    public FlightsValueObject.FlightsId FlightId { get; private set; } = null!;
    public PartialValue PartialValue { get; private set; } = null!;

    private FlightReservation() { }

    public static FlightReservation Create(Guid id, Guid reservationId, Guid flightId, decimal partialValue)
    {
        return new FlightReservation
        {
            Id = FlightReservationId.Create(id),
            ReservationId = ReverseId.Create(reservationId),
            FlightId = FlightsValueObject.FlightsId.Create(flightId),
            PartialValue = PartialValue.Create(partialValue)
        };
    }
}