using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.ValueObject;

public sealed class ContinentsId 
{
    public Guid Value { get; }

    private ContinentsId(Guid value) => Value = value;

    public static ContinentsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del continente no es válido", nameof(value));

        return new ContinentsId(value);
    }
}