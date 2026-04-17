using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.Aggregate;

public sealed class Region
{
    public RegionId Id { get; private set; } = null!;
    public RegionName Name { get; private set; } = null!;
    public RegionType Type { get; private set; } = null!;
    public CountryId CountryId { get; private set; } = null!;

    private Region() { }

    private Region(RegionId id, RegionName name, RegionType type, CountryId countryId)
    {
        Id = id;
        Name = name;
        Type = type;
        CountryId = countryId;
    }

    public static Region Create(int id, string name, string type, int countryId)
    {
        return new Region(
            RegionId.Create(id),
            RegionName.Create(name),
            RegionType.Create(type),
            CountryId.Create(countryId)
        );
    }

    public void UpdateName(string newName)
    {
        Name = RegionName.Create(newName);
    }

    public void UpdateType(string newType)
    {
        Type = RegionType.Create(newType);
    }

    public void UpdateCountry(int newCountryId)
    {
        CountryId = CountryId.Create(newCountryId);
    }
}