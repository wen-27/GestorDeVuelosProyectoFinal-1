namespace GestorDeVuelosProyectoFinal.Moduls.Countries.Infrastructure.Persistence.Entities;

public sealed class CountryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IsoCode { get; set; } = null!;
    public int ContinentId { get; set; }
}