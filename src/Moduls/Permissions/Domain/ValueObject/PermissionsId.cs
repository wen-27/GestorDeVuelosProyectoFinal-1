using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Permissions.Domain.ValueObject;

public sealed class PermissionsId
{
    public Guid Value { get; }
    private PermissionsId(Guid value) => Value = value;

    public static PermissionsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del permiso no es válido.");
        return new PermissionsId(value);
    }
}