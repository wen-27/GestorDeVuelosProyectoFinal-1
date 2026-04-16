using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.Repositories;

public interface IReservationsRepository
{
    Task<Aggregate.Reservations?> GetByIdAsync(ReverseId id);
    Task<IEnumerable<Aggregate.Reservations>> GetAllAsync();
    Task SaveAsync(Aggregate.Reservations reservations);
    Task DeleteAsync(ReverseId id);
}
