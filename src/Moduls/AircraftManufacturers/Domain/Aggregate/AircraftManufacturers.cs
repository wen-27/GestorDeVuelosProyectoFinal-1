using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.Aggregate;

public sealed class AircraftManufacturers
{
    public AircraftManufacturersId Id { get; private set; } = null!;
    public AircraftManufacturersName Name { get; private set; } = null!;
    public AircraftManufacturersCountry Country { get; private set; } = null!;

    private AircraftManufacturers() { }

    private AircraftManufacturers(AircraftManufacturersId id, AircraftManufacturersName name, AircraftManufacturersCountry country)
    {
        Id = id;
        Name = name;
        Country = country;
    }

    public static AircraftManufacturers Create(Guid id, string name, string country)
    {
        return new AircraftManufacturers(
            AircraftManufacturersId.Create(id),
            AircraftManufacturersName.Create(name),
            AircraftManufacturersCountry.Create(country)
        );
    }

    public void UpdateName(string newName)
    {
        Name = AircraftManufacturersName.Create(newName);
    }
}
