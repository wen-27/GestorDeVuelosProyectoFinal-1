using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.Routes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Routes.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Routes.Domain.Repositories;

public interface IRoutesRepository
{
    Task<Route?> GetByIdAsync(RouteId id);
    
  
    Task<Route?> GetByOriginAndDestinationAsync(AirportsId origin, AirportsId destination);

    Task<IEnumerable<Route>> GetAllAsync();
    Task SaveAsync(Route route);
    Task DeleteAsync(RouteId id);
}