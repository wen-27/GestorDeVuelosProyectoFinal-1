namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Infrastructure.Persistence.Entities;

public sealed class AddressEntity
{
    public int Id { get; set; }
    public int StreetTypeId { get; set; }
    public string StreetName { get; set; } = null!;
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public int CityId { get; set; }
    public string? PostalCode { get; set; }
}