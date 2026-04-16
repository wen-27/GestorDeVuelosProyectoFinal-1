using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;

public sealed class CityName
{
    public string Value { get; }
    private CityName(string value) => Value = value;

    public static CityName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre de la ciudad no puede estar vacío", nameof(value));

        if (value.Length > 100 || value.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(value), "El nombre de la ciudad no puede tener más de 100 caracteres y menos de 2.");
        
        if (value.All(char.IsDigit))
            throw new ArgumentException("El nombre de la ciudad no puede contener solo números", nameof(value));

        return new CityName(value.Trim());
    }
}