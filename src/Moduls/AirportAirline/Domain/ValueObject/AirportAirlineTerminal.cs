using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.ValueObject;

public class AirportAirlineTerminal
{
    public string Value { get; }
    private AirportAirlineTerminal(string value) => Value = value;

    public static AirportAirlineTerminal Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El terminal no puede estar vacío", nameof(value));

        if (value.Length > 20)
            throw new ArgumentOutOfRangeException(nameof(value), "El terminal no puede superar los 20 caracteres.");

        return new AirportAirlineTerminal(value.Trim());
    }
}
