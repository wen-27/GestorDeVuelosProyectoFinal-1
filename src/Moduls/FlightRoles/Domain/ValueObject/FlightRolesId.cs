using System;

namespace GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.ValueObject;

public sealed class FlightRolesId
{
    public Guid Value { get; }
    private FlightRolesId(Guid value) => Value = value;
    public static FlightRolesId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID del rol de vuelo no es válido.");
        return new FlightRolesId(value);
    }
}