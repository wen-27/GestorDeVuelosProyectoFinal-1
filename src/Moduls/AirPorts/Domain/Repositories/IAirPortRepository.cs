using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.Repositories;

public interface IAirportsRepository
{
    Task<Airport?> GetByIdAsync(AirportsId id);
    
    Task<Airport?> GetByIataCodeAsync(AirportsIataCode iataCode);
    Task<Airport?> GetByIcaoCodeAsync(AirportsIcaoCode icaoCode);
    
    // CAMBIO AQUÍ: De CitiesId (plural) a CityId (singular)
    Task<IEnumerable<Airport>> GetByCityIdAsync(CityId cityId);

    Task<IEnumerable<Airport>> GetAllAsync();
    
    Task SaveAsync(Airport airport);
    Task DeleteAsync(AirportsId id);
}