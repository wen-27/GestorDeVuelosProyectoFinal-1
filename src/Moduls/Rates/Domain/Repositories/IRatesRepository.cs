using GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.Repositories;

public interface IRatesRepository
{
    Task<Aggregate.Rates?> GetByIdAsync(RatesId id);
    Task<IEnumerable<Aggregate.Rates>> GetAllAsync();
    Task SaveAsync(Aggregate.Rates rates);    
    Task DeleteAsync(RatesId id);
}
