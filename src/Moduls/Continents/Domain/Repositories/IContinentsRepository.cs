using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.Repositories;

public interface IContinentsRepository
{
    Task<Continent?> GetByIdAsync(ContinentsId id);
    Task<IEnumerable<Continent>> GetAllAsync();
    Task SaveAsync(Continent continent);
    Task DeleteAsync(ContinentsId id);
}