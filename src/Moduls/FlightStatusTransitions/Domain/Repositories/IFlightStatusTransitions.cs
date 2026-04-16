using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatusTransitions.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatusTransitions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightStatusTransitions.Domain.Repositories;

public interface IFlightStatusTransitions
{
    Task<Aggregate.FlightStatusTransitions?> GetByIdAsync(FlightStatusTransitionsId id);
    Task<IEnumerable<Aggregate.FlightStatusTransitions>> GetAllAsync();
    Task SaveAsync(Aggregate.FlightStatusTransitions flightStatusTransitions);
    Task DeleteAsync(FlightStatusTransitionsId id);
}
