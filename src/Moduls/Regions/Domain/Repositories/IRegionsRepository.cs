using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.Repositories;

public interface IRegionsRepository
{
    Task<Region?> GetByIdAsync(RegionId id);
    Task<IEnumerable<Region>> GetAllAsync();
    
    // Método clave para obtener departamentos o regiones de un país en específico
    Task<IEnumerable<Region>> GetByCountryAsync(CountryId countryId);
    
    Task SaveAsync(Region region);
    Task DeleteAsync(RegionId id);
}
