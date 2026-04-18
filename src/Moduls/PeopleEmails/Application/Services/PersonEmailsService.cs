using GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Application.UseCases;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Application.Services;

public sealed class PersonEmailsService : IPersonEmailsService
{
    private readonly GetPersonEmailsUseCase _get;
    private readonly CreatePersonEmailUseCase _create;
    private readonly UpdatePersonEmailUseCase _update;
    private readonly DeletePersonEmailUseCase _delete;

    public PersonEmailsService(
        GetPersonEmailsUseCase get,
        CreatePersonEmailUseCase create,
        UpdatePersonEmailUseCase update,
        DeletePersonEmailUseCase delete)
    {
        _get = get;
        _create = create;
        _update = update;
        _delete = delete;
    }

    public Task<IEnumerable<PersonEmail>> GetAllAsync() => _get.ExecuteAsync();

    public Task<IEnumerable<PersonEmail>> GetByPersonIdAsync(int personId) => _get.GetByPersonIdAsync(personId);

    public Task<PersonEmail?> GetByIdAsync(int id) => _get.GetByIdAsync(id);

    public Task CreateAsync(int personId, string emailUser, int emailDomainId, bool isPrimary)
        => _create.ExecuteAsync(personId, emailUser, emailDomainId, isPrimary);

    public Task UpdateAsync(int id, int personId, string emailUser, int emailDomainId, bool isPrimary)
        => _update.ExecuteAsync(id, personId, emailUser, emailDomainId, isPrimary);

    public Task DeleteAsync(int id) => _delete.ExecuteAsync(id);
}
