using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.Repositories;

public interface ICityRepository
{
    Task<City?> GetByIdAsync(CityId id);
    Task<IEnumerable<City>> GetAllAsync();
    
    // Para cargar ciudades cuando el usuario selecciona una región/departamento
    Task<IEnumerable<City>> GetByRegionAsync(RegionId regionId);
    
    Task SaveAsync(City city);
    Task DeleteAsync(CityId id);
}