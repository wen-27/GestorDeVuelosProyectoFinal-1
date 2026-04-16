using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.ValueObject;

public sealed class ReserveStateName
{
    public string Value { get; }

    private ReserveStateName(string value) => Value = value;

    public static ReserveStateName Create(string value)
    {
        if (value.Length > 50)
            throw new ArgumentException("El nombre de la reserva no puede superar los 50 caracteres", nameof(value));

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre de la reserva no es válido", nameof(value));

        return new ReserveStateName(value);
    }
}
