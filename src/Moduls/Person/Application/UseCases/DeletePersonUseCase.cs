using GestorDeVuelosProyectoFinal.Moduls.People.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;

namespace GestorDeVuelosProyectoFinal.Moduls.People.Application.UseCases;

public sealed class DeletePersonUseCase
{
    private readonly IPeopleRepository _repository;
    private readonly IPersonalRepository _personalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonUseCase(IPeopleRepository repository, IPersonalRepository personalRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _personalRepository = personalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteByIdAsync(int id)
    {
        var personId = PeopleId.Create(id);
        var person = await _repository.GetByIdAsync(personId);

        if (person is null)
            throw new InvalidOperationException($"No se encontró la persona con ID {id}.");

        await EnsureNotLinkedAsync(person);
        await _repository.DeleteAsync(personId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> ExecuteByFirstNameAsync(string firstName)
    {
        var people = (await _repository.GetByFirstNameAsync(PeopleNames.Create(firstName))).ToList();
        return await DeleteManyAsync(people, p => _repository.DeleteByFirstNameAsync(PeopleNames.Create(firstName)));
    }

    public async Task<int> ExecuteByLastNameAsync(string lastName)
    {
        var people = (await _repository.GetByLastNameAsync(PeopleLastNames.Create(lastName))).ToList();
        return await DeleteManyAsync(people, p => _repository.DeleteByLastNameAsync(PeopleLastNames.Create(lastName)));
    }

    public async Task<int> ExecuteByDocumentNumberAsync(string documentNumber)
    {
        var people = (await _repository.GetByDocumentNumberAsync(PeopleDocumentNumber.Create(documentNumber))).ToList();
        return await DeleteManyAsync(people, p => _repository.DeleteByDocumentNumberAsync(PeopleDocumentNumber.Create(documentNumber)));
    }

    private async Task<int> DeleteManyAsync(IReadOnlyCollection<Person> people, Func<IReadOnlyCollection<Person>, Task<int>> deleteAction)
    {
        if (people.Count == 0)
            throw new InvalidOperationException("No se encontraron personas para eliminar.");

        foreach (var person in people)
            await EnsureNotLinkedAsync(person);

        var deleted = await deleteAction(people);
        await _unitOfWork.SaveChangesAsync();
        return deleted;
    }

    private async Task EnsureNotLinkedAsync(Person person)
    {
        var linkedStaff = await _personalRepository.GetByPersonIdAsync(person.Id);
        if (linkedStaff is not null)
            throw new InvalidOperationException("No se puede eliminar la persona porque está vinculada a un empleado.");
    }
}
