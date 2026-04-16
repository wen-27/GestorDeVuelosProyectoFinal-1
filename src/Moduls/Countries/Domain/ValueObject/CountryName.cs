using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

public sealed class CountryName
{
    public string Value { get; }
    private CountryName(string value) => Value = value;

    public static CountryName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del país no puede estar vacío", nameof(value));

        if (value.Length > 100 || value.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(value), "El nombre del país no puede tener más de 100 caracteres y menos de 2.");
        
        if (value.All(char.IsDigit))
            throw new ArgumentException("El nombre del país no puede contener solo números", nameof(value));

        return new CountryName(value.Trim());
    }
}