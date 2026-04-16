using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.ValueObject;

public sealed class PartialValue
{
    public decimal Value { get; }
    private PartialValue(decimal value) => Value = value;

    public static PartialValue Create(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("El valor parcial no puede ser negativo.");
        
        return new PartialValue(value);
    }
}