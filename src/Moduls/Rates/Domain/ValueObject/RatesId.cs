using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.ValueObject;

public sealed class RatesId
{
    public Guid Value { get; }

    private RatesId(Guid value) => Value = value;

    public static RatesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la tarifa no es válido", nameof(value));

        return new RatesId(value);
    }
}
