using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Domain.ValueObject;
public sealed class FlightStatuId
{
    public Guid Value { get; }
    private FlightStatuId(Guid value) => Value = value;
    public static FlightStatuId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del estado de vuelo no es válido", nameof(value));

        return new FlightStatuId(value);
    }
}
