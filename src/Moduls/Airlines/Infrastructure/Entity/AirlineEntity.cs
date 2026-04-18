namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.Infrastructure.Persistence.Entities;

public sealed class AirlineEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IataCode { get; set; } = null!;
    public int OriginCountryId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
