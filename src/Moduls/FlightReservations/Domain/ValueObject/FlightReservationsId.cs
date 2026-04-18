using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.ValueObject;

public sealed class FlightReservationId
{
    public int Value { get; }
    private FlightReservationId(int value) => Value = value;

    public static FlightReservationId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("El ID de la relación reserva-vuelo no es válido.");
        return new FlightReservationId(value);
    }
}
