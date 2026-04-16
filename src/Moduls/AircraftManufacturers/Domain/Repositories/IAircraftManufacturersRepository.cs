using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.Repositories;

public interface IAircraftManufacturersRepository
{
    Task<Aggregate.AircraftManufacturers?> GetByIdAsync(AircraftManufacturersId id);
    Task<IEnumerable<Aggregate.AircraftManufacturers>> GetAllAsync();
    Task SaveAsync(Aggregate.AircraftManufacturers aircraftManufacturers);
}
