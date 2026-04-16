using System;

namespace GestorDeVuelosProyectoFinal.Moduls.FlightAssignments.Domain.ValueObject;


public sealed class FlightAssignmentId 
{
    public Guid Value { get; }
    private FlightAssignmentId(Guid value) => Value = value;
    public static FlightAssignmentId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID de asignación de vuelo no es válido.");
        return new FlightAssignmentId(value);
    }
}