using System;
using GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.Aggregate;

public sealed class RouteStopovers
{
    public RouteStopOversId Id { get; private set; } = null!;
    public RouteStopOversSequence Sequence { get; private set; } = null!;
    public RouteStopOversStopDurationMin StopDurationMin { get; private set; } = null!;

    private RouteStopovers() { }

    private RouteStopovers(
        RouteStopOversId id,
        RouteStopOversSequence sequence,
        RouteStopOversStopDurationMin stopDurationMin)
    {
        Id = id;
        Sequence = sequence;
        StopDurationMin = stopDurationMin;
    }

    public static RouteStopovers create(
        Guid id,
        int sequence,
        int stopDurationMin)
    {
        return new RouteStopovers(
            RouteStopOversId.Create(id),
            RouteStopOversSequence.Create(sequence),
            RouteStopOversStopDurationMin.Create(stopDurationMin)
        );
    }
    public void UpdateSequence(int newSequence)
    {
        // El Value Object RouteStopOversSequence se encarga de validar (longitud, números, etc.)
        Sequence = RouteStopOversSequence.Create(newSequence);
    }

    public void UpdateStopDurationMin(int newStopDurationMin)
    {
        // El Value Object RouteStopOversStopDurationMin se encarga de validar (longitud, números, etc.)
        StopDurationMin = RouteStopOversStopDurationMin.Create(newStopDurationMin);
    }
}
