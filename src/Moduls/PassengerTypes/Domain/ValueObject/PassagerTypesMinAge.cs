using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.ValueObject;

public sealed class PassengerMinAge
{
    public int Value { get; }
    private PassengerMinAge(int value) => Value = value;

    public static PassengerMinAge Create(int value)
    {
        if (value < 0) throw new ArgumentException("La edad mínima no puede ser negativa.");
        return new PassengerMinAge(value);
    }
}