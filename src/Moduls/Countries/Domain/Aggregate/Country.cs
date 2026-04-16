using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.ValueObject; // Referencia al otro módulo
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.Aggregate;

public sealed class Country
{
    public CountryId Id { get; private set; } = null!;
    public CountryName Name { get; private set; } = null!;
    public CountryIsoCode IsoCode { get; private set; } = null!;
    
    // Aquí usamos el Value Object del ID del continente como Foreign Key
    public ContinentsId ContinentId { get; private set; } = null!;

    private Country() { }

    private Country(CountryId id, CountryName name, CountryIsoCode isoCode, ContinentsId continentId)
    {
        Id = id;
        Name = name;
        IsoCode = isoCode;
        ContinentId = continentId;
    }

    public static Country Create(Guid id, string name, string isoCode, Guid continentId)
    {
        return new Country(
            CountryId.Create(id),
            CountryName.Create(name),
            CountryIsoCode.Create(isoCode),
            ContinentsId.Create(continentId)
        );
    }
}