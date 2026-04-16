
using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.ValueObject;

public sealed record AirlinesCreatedIn
{
    public DateTime Value { get; }
    private AirlinesCreatedIn(DateTime value) => Value = value;

    public static AirlinesCreatedIn Create(DateTime value)
    {
        if (value == default)
            throw new ArgumentException("La fecha de creación no es válida.");

        if (value > DateTime.Now.AddMinutes(1)) // Margen de 1 min por retrasos de ejecución
            throw new ArgumentException("La fecha de creación no puede ser una fecha futura.");

        return new AirlinesCreatedIn(value);
    }
}