using GestorDeVuelosProyectoFinal.src.Moduls.CheckinStates.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.CheckinStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CheckinStates.Domain.Repositories;

public interface ICheckinStatesRepository
{
    Task<CheckinState?> GetByIdAsync(CheckinStatesId id);
    Task<IEnumerable<CheckinState>> GetAllAsync();
    Task SaveAsync(CheckinState checkinState);
    Task DeleteAsync(CheckinStatesId id);
}