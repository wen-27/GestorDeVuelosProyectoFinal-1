namespace GestorDeVuelosProyectoFinal.Moduls.Airports.Infrastructure.Persistence.Entities;

public sealed class AirportEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IataCode { get; set; } = null!;
    public string? IcaoCode { get; set; }
    public int CityId { get; set; }
}
