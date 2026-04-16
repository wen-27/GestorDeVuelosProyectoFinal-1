using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;

public sealed class CityId 
{
    public Guid Value { get; }

    public  CityId(Guid value) => Value = value;

    public static CityId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la ciudad no es válido", nameof(value));

        return new CityId(value);
    }
}