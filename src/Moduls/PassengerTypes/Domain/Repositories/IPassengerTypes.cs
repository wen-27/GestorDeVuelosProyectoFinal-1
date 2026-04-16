using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Repositories;

public interface IPassengerTypesRepository
{
    Task<PassengerType?> GetByIdAsync(PassengerTypesId id);
    Task<PassengerType?> GetByNameAsync(PassengerTypesName name);
    Task<IEnumerable<PassengerType>> GetAllAsync();
    Task SaveAsync(PassengerType passengerType);
    Task DeleteAsync(PassengerTypesId id);
}