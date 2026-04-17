using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;

public sealed record RoadTypeId
{
    public int Value { get; }
    public RoadTypeId(int value) => Value = value;

    public static RoadTypeId Create(int value)
    {
        if (value == 0)
            throw new ArgumentException("El ID del tipo de vía no es válido.", nameof(value));

        return new RoadTypeId(value);
    }
}