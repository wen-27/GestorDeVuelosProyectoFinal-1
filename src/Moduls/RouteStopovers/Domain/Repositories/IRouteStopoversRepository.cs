using GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.Repositories;

public interface IRouteStopoversRepository
{
    Task<Aggregate.RouteStopovers?> GetByIdAsync(RouteStopOversId id);
    Task<IEnumerable<Aggregate.RouteStopovers>> GetAllAsync();
    Task SaveAsync(Aggregate.RouteStopovers routeStopOvers);
    Task DeleteAsync(RouteStopOversId id);
}
