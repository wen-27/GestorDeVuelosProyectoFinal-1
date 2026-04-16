using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject;

public sealed class AirportsIcaoCode // Cambiado a class para evitar ambigüedad de record
{
    public string? Value { get; }

    private AirportsIcaoCode(string? value) => Value = value;

    public static AirportsIcaoCode Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new AirportsIcaoCode(null);
        }

        var code = value.Trim().ToUpper();
        
        if (code.Length != 4)
        {
            throw new ArgumentException("El código ICAO debe tener exactamente 4 caracteres.");
        }

        return new AirportsIcaoCode(code);
    }
}