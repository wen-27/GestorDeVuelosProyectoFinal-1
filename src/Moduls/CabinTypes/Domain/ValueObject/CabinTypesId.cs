using System;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;

public sealed class CabinTypesId 
{
    public Guid Value { get; }

    private CabinTypesId(Guid value) => Value = value;

    public static CabinTypesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del tipo de cabina no es válido", nameof(value));

        return new CabinTypesId(value);
    }
}