namespace GestorDeVuelosProyectoFinal.Moduls.Regions.Infrastructure.Persistence.Entities;

public sealed class RegionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int CountryId { get; set; }
}