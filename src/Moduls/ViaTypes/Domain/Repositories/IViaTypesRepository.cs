using GestorDeVuelosProyectoFinal.Moduls.ViaTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.ViaTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.ViaTypes.Domain.Repositories;

public interface IViaTypesRepository
{
    Task<ViaType?> GetByIdAsync(ViaTypesId id);
    Task<IEnumerable<ViaType>> GetAllAsync();
    Task SaveAsync(ViaType viaType);
    Task DeleteAsync(ViaTypesId id);
}