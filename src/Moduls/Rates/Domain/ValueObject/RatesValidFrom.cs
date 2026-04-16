using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.ValueObject;

public sealed class RatesValidFrom
{
    public DateOnly Value { get; }

    private RatesValidFrom(DateOnly value) => Value = value;

    public static RatesValidFrom Create(DateOnly value)
    {
        if (value == DateOnly.MinValue)
            throw new ArgumentException("La fecha de vencimiento de la tarifa no es válida", nameof(value));

        return new RatesValidFrom(value);
    }
}