using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.Aggregate;

public sealed class City
{
    public CityId Id { get; private set; } = null!;
    public CityName Name { get; private set; } = null!;
    public RegionId RegionId { get; private set; } = null!;

    // Constructor privado para persistencia/EF
    private City() { }

    // Constructor privado para el factory method
    private City(CityId id, CityName name, RegionId regionId)
    {
        Id = id;
        Name = name;
        RegionId = regionId;
    }

    public static City Create(int id, string name, int regionId)
    {
        return new City(
            CityId.Create(id),
            CityName.Create(name),
            RegionId.Create(regionId)
        );
    }

    public void UpdateName(string newName)
    {
        Name = CityName.Create(newName);
    }

    public void UpdateRegion(int newRegionId)
    {
        RegionId = RegionId.Create(newRegionId);
    }
}