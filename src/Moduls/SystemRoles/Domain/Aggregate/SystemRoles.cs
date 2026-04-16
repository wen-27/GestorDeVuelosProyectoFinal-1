using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Roles.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Roles.Domain.Aggregate;

public sealed class SystemRoles
{
    public RolesId Id { get; private set; } = null!;
    public RolesName Name { get; private set; } = null!;
    public RolesDescription Description { get; private set; } = null!;

    private SystemRoles() { }

    public static SystemRoles Create(Guid id, string name, string? description)
    {
        return new SystemRoles
        {
            Id = RolesId.Create(id),
            Name = RolesName.Create(name),
            Description = RolesDescription.Create(description)
        };
    }
}