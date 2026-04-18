using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Infrastructure.Repository;

public sealed class DocumentTypesRepository : IDocumentTypesRepository
{
    private readonly AppDbContext _context;

    public DocumentTypesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentType?> GetByIdAsync(DocumentTypesId id)
    {
        var entity = await _context.DocumentTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<DocumentType?> GetByNameAsync(DocumentTypesName name)
    {
        var normalized = name.Value.Trim().ToLower();
        var entity = await _context.DocumentTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.ToLower() == normalized);

        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<DocumentType?> GetByCodeAsync(DocumentTypesCode code)
    {
        var entity = await _context.DocumentTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Code == code.Value);

        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<DocumentType?> GetByNameStringAsync(string name)
    {
        return await GetByNameAsync(DocumentTypesName.Create(name));
    }

    public async Task<DocumentType?> GetByCodeStringAsync(string code)
    {
        return await GetByCodeAsync(DocumentTypesCode.Create(code));
    }

    public async Task<IEnumerable<DocumentType>> GetAllAsync()
    {
        var entities = await _context.DocumentTypes
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return entities.Select(MapToDomain);
    }

    public async Task SaveAsync(DocumentType documentType)
    {
        await _context.DocumentTypes.AddAsync(MapToEntity(documentType));
    }

    public Task UpdateAsync(DocumentType documentType)
    {
        _context.DocumentTypes.Update(MapToEntity(documentType));
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(DocumentTypesId id)
    {
        var entity = await _context.DocumentTypes.FindAsync(id.Value);
        if (entity is not null)
            _context.DocumentTypes.Remove(entity);
    }

    public async Task DeleteByNameAsync(DocumentTypesName name)
    {
        var normalized = name.Value.Trim().ToLower();
        var entity = await _context.DocumentTypes
            .FirstOrDefaultAsync(x => x.Name.ToLower() == normalized);

        if (entity is not null)
            _context.DocumentTypes.Remove(entity);
    }

    public async Task DeleteByCodeAsync(DocumentTypesCode code)
    {
        var entity = await _context.DocumentTypes
            .FirstOrDefaultAsync(x => x.Code == code.Value);

        if (entity is not null)
            _context.DocumentTypes.Remove(entity);
    }

    private static DocumentType MapToDomain(DocumentTypeEntity entity)
    {
        return DocumentType.FromPrimitives(entity.Id, entity.Name, entity.Code);
    }

    private static DocumentTypeEntity MapToEntity(DocumentType aggregate)
    {
        return new DocumentTypeEntity
        {
            Id = aggregate.Id?.Value ?? 0,
            Name = aggregate.Name.Value,
            Code = aggregate.Code.Value
        };
    }
}
