using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Domain.Repositories;

public interface IFlightStatusRepository
{
    Task<Aggregate.FlightStatus?> GetByIdAsync(FlightStatuId id);
    Task<IEnumerable<Aggregate.FlightStatus>> GetAllAsync();
    Task SaveAsync(Aggregate.FlightStatus flightStatus);
    Task DeleteAsync(FlightStatuId id);
}
