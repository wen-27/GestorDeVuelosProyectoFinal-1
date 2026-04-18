namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.Infrastructure.Entities;

public sealed class SeasonEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal PriceFactor { get; set; }
}
