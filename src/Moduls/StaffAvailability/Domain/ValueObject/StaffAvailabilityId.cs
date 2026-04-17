using System;

namespace GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.ValueObject;

public sealed class StaffAvailabilityId
{
    public int Value { get; }
    private StaffAvailabilityId(int value) => Value = value;
    public static StaffAvailabilityId Create(int value)
    {
        if (value == int.Empty) throw new ArgumentException("El ID de disponibilidad no es válido.");
        return new StaffAvailabilityId(value);
    }
}