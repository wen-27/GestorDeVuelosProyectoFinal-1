using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.Repositories;

public interface IPersonEmailsRepository
{
    Task<PersonEmail?> GetByIdAsync(PersonEmailsId id);
    Task<IEnumerable<PersonEmail>> GetByPersonIdAsync(PeopleId personId);
    Task<PersonEmail?> GetPrimaryByPersonIdAsync(PeopleId personId);
    Task SaveAsync(PersonEmail personEmail);
    Task DeleteAsync(PersonEmailsId id);
}