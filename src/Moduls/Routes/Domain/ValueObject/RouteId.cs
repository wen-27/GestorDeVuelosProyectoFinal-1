using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Routes.Domain.ValueObject;

public sealed class RouteId
{
    public Guid Value { get; }
    private RouteId(Guid value) => Value = value;
    public static RouteId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID de la ruta no es válido.");
        return new RouteId(value);
    }
}