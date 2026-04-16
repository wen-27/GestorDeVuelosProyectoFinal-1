using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;
public sealed class RegionId 
{
    public Guid Value { get; }

    private RegionId(Guid value) => Value = value;

    public static RegionId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la región no es válido", nameof(value));

        return new RegionId(value);
    }
}