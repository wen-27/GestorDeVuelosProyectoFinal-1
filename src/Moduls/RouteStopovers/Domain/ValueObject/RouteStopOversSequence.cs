using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.ValueObject;

public sealed class RouteStopOversSequence
{
    public int Value { get; }

    private RouteStopOversSequence(int value) => Value = value;

    public static RouteStopOversSequence Create(int value)
    {
        if (value < 1)
            throw new ArgumentException("La secuencia de paradas no es válida", nameof(value));

        return new RouteStopOversSequence(value);
    }
}
