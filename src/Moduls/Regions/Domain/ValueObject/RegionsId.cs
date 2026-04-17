using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;
public sealed class RegionId
{
    public int Value { get; }
    private RegionId(int value) => Value = value;
    public static RegionId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("El id de la región no es válido", nameof(value));
        return new RegionId(value);
    }
}