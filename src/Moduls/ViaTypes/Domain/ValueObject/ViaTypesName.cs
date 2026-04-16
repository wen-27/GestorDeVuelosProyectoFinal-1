using System;

namespace GestorDeVuelosProyectoFinal.Moduls.ViaTypes.Domain.ValueObject;

public sealed class ViaTypesName
{
    public string Value { get; }
    private ViaTypesName(string value) => Value = value;

    public static ViaTypesName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del tipo de via no puede estar vacío", nameof(value));

        if (value.Length > 100 || value.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(value), "El nombre del tipo de via no puede tener más de 100 caracteres y menos de 2.");
        
        if (value.All(char.IsDigit))
            throw new ArgumentException("El nombre del tipo de via no puede contener solo números", nameof(value));

        return new ViaTypesName(value.Trim());
    }
}