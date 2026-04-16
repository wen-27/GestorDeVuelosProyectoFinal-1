using System;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.Aggregate;

public sealed class Airport
{
    public AirportsId Id { get; private set; } = null!;
    public AirportsName Name { get; private set; } = null!;
    public AirportsIataCode IataCode { get; private set; } = null!;
    public AirportsIcaoCode IcaoCode { get; private set; } = null!;
    public CityId CityId { get; private set; } = null!; // <--- REVISA SI ES CityId o CitiesId (como el de países)

    private Airport() { }

    public static Airport Create(Guid id, string name, string iataCode, string? icaoCode, Guid cityId)
    {
        return new Airport
        {
            Id = AirportsId.Create(id),
            Name = AirportsName.Create(name),
            IataCode = AirportsIataCode.Create(iataCode),
            IcaoCode = AirportsIcaoCode.Create(icaoCode),
            CityId = CityId.Create(cityId) // <--- Asegúrate que coincida con tu clase de ciudades
        };
    }
}