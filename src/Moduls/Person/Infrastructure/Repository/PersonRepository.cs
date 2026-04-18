using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.People.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace GestorDeVuelosProyectoFinal.Moduls.People.Infrastructure.Repository;

public sealed class PersonRepository : IPeopleRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(PeopleId id)
    {
        var entity = await _context.Persons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id.Value);
        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<Person?> GetByDocumentAsync(DocumentTypesId documentTypeId, PeopleDocumentNumber documentNumber)
    {
        var entity = await _context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DocumentTypeId == documentTypeId.Value && x.DocumentNumber == documentNumber.Value);

        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<IEnumerable<Person>> GetByDocumentNumberAsync(PeopleDocumentNumber documentNumber)
    {
        var entities = await _context.Persons
            .AsNoTracking()
            .Where(x => x.DocumentNumber == documentNumber.Value)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync();

        return entities.Select(MapToDomain);
    }

    public async Task<IEnumerable<Person>> GetByFirstNameAsync(PeopleNames firstName)
    {
        var normalized = firstName.Value.Trim().ToLower();
        var entities = await _context.Persons
            .AsNoTracking()
            .Where(x => x.FirstName.ToLower().Contains(normalized))
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToListAsync();

        return entities.Select(MapToDomain);
    }

    public async Task<IEnumerable<Person>> GetByLastNameAsync(PeopleLastNames lastName)
    {
        var normalized = lastName.Value.Trim().ToLower();
        var entities = await _context.Persons
            .AsNoTracking()
            .Where(x => x.LastName.ToLower().Contains(normalized))
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync();

        return entities.Select(MapToDomain);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        var entities = await _context.Persons
            .AsNoTracking()
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync();

        return entities.Select(MapToDomain);
    }

    public async Task SaveAsync(Person person)
    {
        var entity = MapToEntity(person);
        await _context.Persons.AddAsync(entity);
    }

    public Task UpdateAsync(Person person)
    {
        _context.Persons.Update(MapToEntity(person));
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(PeopleId id)
    {
        var entity = await _context.Persons.FindAsync(id.Value);
        if (entity is not null)
            _context.Persons.Remove(entity);
    }

    public async Task<int> DeleteByDocumentNumberAsync(PeopleDocumentNumber documentNumber)
    {
        var entities = await _context.Persons
            .Where(x => x.DocumentNumber == documentNumber.Value)
            .ToListAsync();

        _context.Persons.RemoveRange(entities);
        return entities.Count;
    }

    public async Task<int> DeleteByFirstNameAsync(PeopleNames firstName)
    {
        var normalized = firstName.Value.Trim().ToLower();
        var entities = await _context.Persons
            .Where(x => x.FirstName.ToLower().Contains(normalized))
            .ToListAsync();

        _context.Persons.RemoveRange(entities);
        return entities.Count;
    }

    public async Task<int> DeleteByLastNameAsync(PeopleLastNames lastName)
    {
        var normalized = lastName.Value.Trim().ToLower();
        var entities = await _context.Persons
            .Where(x => x.LastName.ToLower().Contains(normalized))
            .ToListAsync();

        _context.Persons.RemoveRange(entities);
        return entities.Count;
    }

    private static Person MapToDomain(PersonEntity entity)
    {
        return Person.FromPrimitives(
            entity.Id,
            entity.DocumentTypeId,
            entity.DocumentNumber,
            entity.FirstName,
            entity.LastName,
            entity.BirthDate,
            entity.Gender,
            entity.AddressId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    private static PersonEntity MapToEntity(Person aggregate)
    {
        return new PersonEntity
        {
            Id = aggregate.Id?.Value ?? 0,
            DocumentTypeId = aggregate.DocumentTypeId.Value,
            DocumentNumber = aggregate.DocumentNumber.Value,
            FirstName = aggregate.FirstName.Value,
            LastName = aggregate.LastNames.Value,
            BirthDate = aggregate.BirthDate.Value,
            Gender = aggregate.Gender.Value,
            AddressId = aggregate.AddressId?.Value,
            CreatedAt = aggregate.CreatedAt.Value,
            UpdatedAt = aggregate.UpdatedAt.Value
        };
    }
}
