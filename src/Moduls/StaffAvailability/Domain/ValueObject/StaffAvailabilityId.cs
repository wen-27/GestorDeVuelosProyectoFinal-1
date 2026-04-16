using System;

namespace GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.ValueObject;

public sealed class StaffAvailabilityId
{
    public Guid Value { get; }
    private StaffAvailabilityId(Guid value) => Value = value;
    public static StaffAvailabilityId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID de disponibilidad no es válido.");
        return new StaffAvailabilityId(value);
    }
}