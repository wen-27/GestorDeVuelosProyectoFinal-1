using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.ValueObject;

public sealed class RouteStopOversId
{
    public Guid Value { get; }

    private RouteStopOversId(Guid value) => Value = value;

    public static RouteStopOversId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la parada no es válido", nameof(value));

        return new RouteStopOversId(value);
    }
}
