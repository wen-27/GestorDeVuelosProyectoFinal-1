using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.RolePermissions.Domain.ValueObject;

public sealed class RolePermissionsId
{
    public Guid Value { get; }
    private RolePermissionsId(Guid value) => Value = value;

    public static RolePermissionsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la relación rol-permiso no es válido.");
        return new RolePermissionsId(value);
    }
}