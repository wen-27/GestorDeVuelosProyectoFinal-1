using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.ValueObject;

public sealed class PassengerTypesName
{
    public string Value { get; }
    private PassengerTypesName(string value) => Value = value;

    public static PassengerTypesName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del tipo de pasajero es obligatorio.");
        return new PassengerTypesName(value.Trim());
    }
}