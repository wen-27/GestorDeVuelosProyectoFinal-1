using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.ValueObject;

public sealed class FlightReservationId
{
    public Guid Value { get; }
    private FlightReservationId(Guid value) => Value = value;

    public static FlightReservationId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El ID de la relación reserva-vuelo no es válido.");
        return new FlightReservationId(value);
    }
}