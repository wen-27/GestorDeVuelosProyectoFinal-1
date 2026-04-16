using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.Repositories;

public interface IFlightRolesRepository
{
    
    Task<FlightRole?> GetByIdAsync(FlightRolesId id);
    Task<IEnumerable<FlightRole>> GetAllAsync();
    Task SaveAsync(FlightRole flightRole);
    Task DeleteAsync(FlightRolesId id);
}