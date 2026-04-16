using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Permissions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Permissions.Domain.Aggregate;

public sealed class Permission
{
    public PermissionsId Id { get; private set; } = null!;
    public PermissionsName Name { get; private set; } = null!;
    public PermissionsDescription Description { get; private set; } = null!;

    private Permission() { }

    public static Permission Create(Guid id, string name, string? description)
    {
        return new Permission
        {
            Id = PermissionsId.Create(id),
            Name = PermissionsName.Create(name),
            Description = PermissionsDescription.Create(description)
        };
    }
}