using System;

namespace GestorDeVuelosProyectoFinal.Moduls.SeatLocationTypes.Domain.ValueObject;

public sealed class SeatLocationTypesId
{
    public Guid Value { get; }
    private SeatLocationTypesId(Guid value) => Value = value;
    public static SeatLocationTypesId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID del tipo de ubicación de asientos no es válido.");
        return new SeatLocationTypesId(value);
    }
}