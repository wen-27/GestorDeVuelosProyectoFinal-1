using System;
using System.Linq;

namespace GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.ValueObject;

public sealed class FlightRolesName
{
    public string Value { get; }
    private FlightRolesName(string value) => Value = value;

    public static FlightRolesName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del rol de vuelo no puede estar vacío", nameof(value));

        if (value.Length > 100 || value.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(value), "El nombre del rol de vuelo no puede tener más de 100 caracteres y menos de 2.");
        
        if (value.All(char.IsDigit))
            throw new ArgumentException("El nombre del rol de vuelo no puede contener solo números", nameof(value));

        return new FlightRolesName(value.Trim());
    }
}