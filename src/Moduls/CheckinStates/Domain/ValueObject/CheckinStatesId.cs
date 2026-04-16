using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CheckinStates.Domain.ValueObject;

public sealed class CheckinStatesId
{
    public Guid Value { get; }

    private CheckinStatesId(Guid value) => Value = value;

    public static CheckinStatesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del estado de check-in no es válido", nameof(value));

        return new CheckinStatesId(value);
    }
}