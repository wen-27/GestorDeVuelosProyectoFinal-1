using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;

public sealed record RoadTypeId
{
    public Guid Value { get; }
    public RoadTypeId(Guid value) => Value = value;

    public static RoadTypeId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El ID del tipo de vía no es válido.", nameof(value));

        return new RoadTypeId(value);
    }
}