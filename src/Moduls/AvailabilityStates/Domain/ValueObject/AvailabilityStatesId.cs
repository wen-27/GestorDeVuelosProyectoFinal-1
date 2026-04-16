using System;

namespace GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.ValueObject;

public sealed class AvailabilityStatesId 
{
    public Guid Value { get; }

    private AvailabilityStatesId(Guid value) => Value = value;

    public static AvailabilityStatesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del estado de disponibilidad no es válido", nameof(value));

        return new AvailabilityStatesId(value);
    }
}