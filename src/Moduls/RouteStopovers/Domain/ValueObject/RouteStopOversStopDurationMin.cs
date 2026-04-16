using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.ValueObject;

public sealed class RouteStopOversStopDurationMin
{
    public int Value { get; }

    private RouteStopOversStopDurationMin(int value) => Value = value;

    public static RouteStopOversStopDurationMin Create(int value)
    {
        if (value < 0)
            throw new ArgumentException("El tiempo de parada no es válido", nameof(value));

        return new RouteStopOversStopDurationMin(value);
    }
}
