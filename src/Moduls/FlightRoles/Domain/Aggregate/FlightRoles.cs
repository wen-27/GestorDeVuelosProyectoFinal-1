using System;
using GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.Aggregate;


public sealed class FlightRole 
{
    public FlightRolesId Id { get; private set; } = null!;
    public FlightRolesName Name { get; private set; } = null!;

    private FlightRole() { }

    public static FlightRole Create(Guid id, string name)
    {
        return new FlightRole
        {
            Id = FlightRolesId.Create(id),
            Name = FlightRolesName.Create(name)
        };
    }

    public void UpdateName(string newName)
    {
        Name = FlightRolesName.Create(newName);
    }
}