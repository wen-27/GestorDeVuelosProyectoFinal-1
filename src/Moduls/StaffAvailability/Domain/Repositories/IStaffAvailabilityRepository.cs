using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.Repositories;

public interface IStaffAvailabilityRepository
{

    Task<StaffAvailabilityRecord?> GetByIdAsync(StaffAvailabilityId id);
    
    Task<IEnumerable<StaffAvailabilityRecord>> GetByStaffIdAsync(PersonalId staffId);

    Task<IEnumerable<StaffAvailabilityRecord>> GetAllAsync();
    
    Task SaveAsync(StaffAvailabilityRecord availability);
    Task DeleteAsync(StaffAvailabilityId id);
}