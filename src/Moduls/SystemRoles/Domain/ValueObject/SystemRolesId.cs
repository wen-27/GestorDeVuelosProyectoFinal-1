using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Roles.Domain.ValueObject;

public sealed class RolesId
{
    public Guid Value { get; }
    private RolesId(Guid value) => Value = value;

    public static RolesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del rol no es válido.");
        return new RolesId(value);
    }
}