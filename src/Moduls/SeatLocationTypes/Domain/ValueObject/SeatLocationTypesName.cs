using System;

namespace GestorDeVuelosProyectoFinal.Moduls.SeatLocationTypes.Domain.ValueObject;

public sealed class SeatLocationTypesName
{
    public string Value { get; }
    private SeatLocationTypesName(string value) => Value = value;

    public static SeatLocationTypesName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del tipo de ubicación de asientos no puede estar vacío", nameof(value));

        if (value.Length > 100 || value.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(value), "El nombre del tipo de ubicación de asientos no puede tener más de 100 caracteres y menos de 2.");
        
        if (value.All(char.IsDigit))
            throw new ArgumentException("El nombre del tipo de ubicación de asientos no puede contener solo números", nameof(value));

        return new SeatLocationTypesName(value.Trim());
    }
}