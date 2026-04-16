using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject;

public sealed record AirportsIataCode
{
    public string Value { get; }
    private AirportsIataCode(string value) => Value = value;

    public static AirportsIataCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código IATA del aeropuerto es obligatorio.");

        var code = value.Trim().ToUpper();
        if (code.Length != 3)
            throw new ArgumentException("El código IATA debe tener exactamente 3 caracteres.");

        return new AirportsIataCode(code);
    }
}