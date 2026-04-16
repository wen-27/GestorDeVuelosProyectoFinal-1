using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.People.Domain.Repositories;

public interface IPeopleRepository
{
    Task<Person?> GetByIdAsync(PeopleId id);
    Task<Person?> GetByDocumentAsync(DocumentTypesId documentTypeId, PeopleDocumentNumber documentNumber);
    Task<IEnumerable<Person>> GetAllAsync();
    Task SaveAsync(Person person);
    Task DeleteAsync(PeopleId id);
    Task<IEnumerable<Person>> SearchByNameAsync(string searchTerm);
}