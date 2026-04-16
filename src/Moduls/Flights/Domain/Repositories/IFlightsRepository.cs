using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.Repositories;

public interface IFlightsRepository
{
    Task<Aggregate.Flights?> GetByIdAsync(FlightsId id);
    Task<IEnumerable<Aggregate.Flights>> GetAllAsync();
    Task SaveAsync(Aggregate.Flights flights);
    Task DeleteAsync(FlightsId id);
}
