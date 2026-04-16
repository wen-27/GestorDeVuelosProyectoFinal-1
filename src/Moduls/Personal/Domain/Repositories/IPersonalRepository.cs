using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.Repositories;

public interface IPersonalRepository
{
    Task<Staff?> GetByIdAsync(PersonalId id);
    Task<Staff?> GetByPersonIdAsync(PeopleId personId); // Ajusta a PeopleId o PersonId
    Task<IEnumerable<Staff>> GetAllAsync();
    Task SaveAsync(Staff staff);
    Task DeleteAsync(PersonalId id);
}