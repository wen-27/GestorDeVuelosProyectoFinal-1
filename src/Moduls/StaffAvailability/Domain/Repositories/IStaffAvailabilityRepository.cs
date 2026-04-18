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
    Task<IEnumerable<StaffAvailabilityRecord>> GetByDateRangeAsync(DateTime startsAt, DateTime endsAt);
    Task<IEnumerable<StaffAvailabilityRecord>> GetAllAsync();
    Task<bool> HasBlockingAvailabilityAsync(PersonalId staffId, DateTime startsAt, DateTime endsAt);
    Task SaveAsync(StaffAvailabilityRecord availability);
    Task UpdateAsync(StaffAvailabilityRecord availability);
    Task DeleteByIdAsync(StaffAvailabilityId id);
    Task<int> DeleteByStaffIdAsync(PersonalId staffId);
}
