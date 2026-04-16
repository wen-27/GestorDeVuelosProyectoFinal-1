using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.ValueObject;

public sealed class PassengersId
{
    public Guid Value { get; }

    private PassengersId(Guid value) => Value = value;

    public static PassengersId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del pasajero no es válido", nameof(value));

        return new PassengersId(value);
    }
}
