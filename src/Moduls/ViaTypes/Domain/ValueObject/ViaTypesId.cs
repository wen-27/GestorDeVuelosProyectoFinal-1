using System;

namespace GestorDeVuelosProyectoFinal.Moduls.ViaTypes.Domain.ValueObject;

public sealed class ViaTypesId 
{
    public Guid Value { get; }

    private ViaTypesId(Guid value) => Value = value;

    public static ViaTypesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del tipo de via no es válido", nameof(value));

        return new ViaTypesId(value);
    }
}