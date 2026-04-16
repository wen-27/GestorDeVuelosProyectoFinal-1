using System;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.Aggregate;

public sealed class City
{
    public CityId Id { get; private set; } = null!;
    public CityName Name { get; private set; } = null!;
    
    // Referencia al ID de la región (Foreign Key en el dominio)
    public RegionId RegionId { get; private set; } = null!;

    private City() { }

    private City(CityId id, CityName name, RegionId regionId)
    {
        Id = id;
        Name = name;
        RegionId = regionId;
    }

    public static City Create(Guid id, string name, Guid regionId)
    {
        return new City(
            CityId.Create(id),
            CityName.Create(name),
            RegionId.Create(regionId)
        );
    }
}