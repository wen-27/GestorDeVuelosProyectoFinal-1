using System;
using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.Aggregate;

public sealed class AvailabilityState
{
    public AvailabilityStatesId Id { get; private set; } = null!;
    public AvailabilityStatesName Name { get; private set; } = null!;

    private AvailabilityState() { }

    public static AvailabilityState Create(Guid id, string name)
    {
        return new AvailabilityState
        {
            Id = AvailabilityStatesId.Create(id),
            Name = AvailabilityStatesName.Create(name)
        };
    }
}