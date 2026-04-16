using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

public sealed class AircraftRegistration
{
    public string Value { get; }
    private AircraftRegistration(string value) => Value = value;

    public static AircraftRegistration Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El número de registro del avión no puede estar vacío", nameof(value));

        if (value.Length > 20)
            throw new ArgumentOutOfRangeException(nameof(value), "El número de registro del avión no puede superar los 20 caracteres.");

        return new AircraftRegistration(value.Trim());
    }
}

