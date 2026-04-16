using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.ValueObject;

public sealed class AirlinesId 
{
    public Guid Value { get; }

    private AirlinesId(Guid value) => Value = value;

    public static AirlinesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del aerolínea no es válido", nameof(value));

        return new AirlinesId(value);
    }
}