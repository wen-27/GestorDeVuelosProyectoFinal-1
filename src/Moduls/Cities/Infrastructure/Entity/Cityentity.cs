namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Infrastructure.Persistence.Entities;

public sealed class CityEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int RegionId { get; set; }
}