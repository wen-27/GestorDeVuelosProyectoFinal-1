namespace GestorDeVuelosProyectoFinal.Moduls.Routes.Infrastructure.Entities;

public sealed class RouteEntity
{
    public int Id { get; set; }
    public int OriginAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public int? DistanceKm { get; set; }
    public int? EstimatedDurationMin { get; set; }
}
