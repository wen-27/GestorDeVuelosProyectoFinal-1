using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Checkins.Domain.ValueObject;

public sealed class CheckinsId
{
    public Guid Value { get; }

    private CheckinsId(Guid value) => Value = value;

    public static CheckinsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del check-in no es válido.");

        return new CheckinsId(value);
    }
}