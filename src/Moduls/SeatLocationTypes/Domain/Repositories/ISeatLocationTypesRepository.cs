using GestorDeVuelosProyectoFinal.Moduls.SeatLocationTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.SeatLocationTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.SeatLocationTypes.Domain.Repositories;

public interface ISeatLocationTypesRepository
{
    Task<SeatLocationType?> GetByIdAsync(SeatLocationTypesId id);
    Task<IEnumerable<SeatLocationType>> GetAllAsync();
    Task SaveAsync(SeatLocationType seatLocationType);
    Task DeleteAsync(SeatLocationTypesId id);
}