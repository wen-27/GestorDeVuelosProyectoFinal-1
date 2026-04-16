using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject; // Referencia al país

namespace GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.Aggregate;
public sealed class Region
{
    public RegionId Id { get; private set; } = null!;
    public RegionName Name { get; private set; } = null!;
    
    // Foreign Key: Referencia al ID del País
    public CountryId CountryId { get; private set; } = null!;

    private Region() { }

    private Region(RegionId id, RegionName name, CountryId countryId)
    {
        Id = id;
        Name = name;
        CountryId = countryId;
    }

    public static Region Create(Guid id, string name, Guid countryId)
    {
        return new Region(
            RegionId.Create(id),
            RegionName.Create(name),
            CountryId.Create(countryId)
        );
    }

    public void UpdateName(string newName)
    {
        Name = RegionName.Create(newName);
    }
}