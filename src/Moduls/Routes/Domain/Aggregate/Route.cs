using System;
using GestorDeVuelosProyectoFinal.Moduls.Routes.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject; // Tu namespace

namespace GestorDeVuelosProyectoFinal.Moduls.Routes.Domain.Aggregate;

public sealed class Route
{
    public RouteId Id { get; private set; } = null!;

    public AirportsId OriginAirportId { get; private set; } = null!; 
    public AirportsId DestinationAirportId { get; private set; } = null!;
    public RouteDistance Distance { get; private set; } = null!;
    public RouteDuration Duration { get; private set; } = null!;

    private Route() { }

    public static Route Create(
        Guid id,
        Guid originId,
        Guid destinationId,
        int? distanceKm,
        int? durationMin)
    {
        if (originId == destinationId)
            throw new ArgumentException("El aeropuerto de origen no puede ser igual al de destino.");

        return new Route
        {
            Id = RouteId.Create(id),

            OriginAirportId = AirportsId.Create(originId),
            DestinationAirportId = AirportsId.Create(destinationId),
            Distance = RouteDistance.Create(distanceKm),
            Duration = RouteDuration.Create(durationMin)
        };
    }
}