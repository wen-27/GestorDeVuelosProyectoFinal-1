using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Domain.ValueObject;

public sealed class FlightStatuName
{
    public string Value { get; }
    private FlightStatuName(string value) => Value = value;
    public static FlightStatuName Create(string value)
    {
        if (value.Length > 50)
            throw new ArgumentException("El nombre del estado de vuelo no puede superar los 50 caracteres", nameof(value));
            
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del estado de vuelo no es válido", nameof(value));
        return new FlightStatuName(value);
    }
}

