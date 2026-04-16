using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.ValueObject;

public sealed class RatesValidTo
{
    public DateOnly Value { get; }

    private RatesValidTo(DateOnly value) => Value = value;

    public static RatesValidTo Create(DateOnly value)
    {
        if (value == DateOnly.MaxValue)
            throw new ArgumentException("La fecha de vencimiento de la tarifa no es válida", nameof(value));

        return new RatesValidTo(value);
    }
}
