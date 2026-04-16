using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.ValueObject;

public sealed class PassengerTypesId
{
    public Guid Value { get; }
    private PassengerTypesId(Guid value) => Value = value;

    public static PassengerTypesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El ID del tipo de pasajero no es válido.");
        return new PassengerTypesId(value);
    }
}