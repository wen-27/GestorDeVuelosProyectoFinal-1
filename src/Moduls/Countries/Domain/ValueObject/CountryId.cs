using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

public sealed class CountryId 
{
    public Guid Value { get; }

    private CountryId(Guid value) => Value = value;

    public static CountryId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del país no es válido", nameof(value));

        return new CountryId(value);
    }
}