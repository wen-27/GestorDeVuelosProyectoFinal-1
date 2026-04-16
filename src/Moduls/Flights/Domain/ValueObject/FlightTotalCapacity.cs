using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightTotalCapacity
{
    public int Value { get; }

    private FlightTotalCapacity(int value) => Value = value;

    public static FlightTotalCapacity Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("El total de capacidad del vuelo no es válido", nameof(value));
        
        if (value > 900)
        throw new ArgumentException("La capacidad del vuelo no puede superar los 900 asientos", nameof(value));


        return new FlightTotalCapacity(value);
    }
}
