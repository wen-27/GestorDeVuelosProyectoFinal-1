using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightAvailableSeats
{
    public int Value { get; }

    private FlightAvailableSeats(int value) => Value = value;

    public static FlightAvailableSeats Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("El número de asientos disponibles del vuelo no es válido", nameof(value));
        
        if (value > 900)
            throw new ArgumentException("El número de asientos disponibles del vuelo no puede superar los 900", nameof(value));

        return new FlightAvailableSeats(value);
    }
}
