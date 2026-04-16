using System;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Domain.Aggregate;

public sealed class FlightStatus
{
    public FlightStatuId Id { get; private set; } = null!;
    public FlightStatuName Name { get; private set; } = null!;

    private FlightStatus() { }

    private FlightStatus(
        FlightStatuId id,
        FlightStatuName name)
    {
        Id = id;
        Name = name;
    }

    public static FlightStatus Create(
        Guid id,
        string name)
    {
        return new FlightStatus(
            FlightStatuId.Create(id),
            FlightStatuName.Create(name)
        );
    }

    public void UpdateName(string newName)
    {
        // El Value Object FlightStatuName se encarga de validar (longitud, números, etc.)
        Name = FlightStatuName.Create(newName);
    }
}
