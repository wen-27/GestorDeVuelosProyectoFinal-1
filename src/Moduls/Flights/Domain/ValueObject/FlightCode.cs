using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightCode
{
    public string Value { get; }

    private FlightCode(string value) => Value = value;

    public static FlightCode Create(string value)
    {
        if (value.Length > 10)
            throw new ArgumentException("El código del vuelo no puede superar los 10 caracteres", nameof(value));

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código del vuelo no es válido", nameof(value));

        return new FlightCode(value);
    }
}
