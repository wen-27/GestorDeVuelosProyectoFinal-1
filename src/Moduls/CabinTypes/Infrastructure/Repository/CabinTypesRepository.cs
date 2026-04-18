using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinTypes.Infrastructure.Repository;

public sealed class CabinTypesRepository : ICabinTypesRepository
{
    private readonly AppDbContext _context;

    public CabinTypesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CabinType?> GetByIdAsync(CabinTypesId id)
    {
        return await _context.Set<CabinType>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CabinType?> GetByIdIntAsync(int id)
    {
        // Usamos el método Create del Value Object para la comparación
        return await _context.Set<CabinType>()
            .FirstOrDefaultAsync(x => x.Id == CabinTypesId.Create(id));
    }

    public async Task<CabinType?> GetByNameAsync(CabinTypesName name)
    {
        return await _context.Set<CabinType>()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<CabinType?> GetByNameStringAsync(string name)
    {
        return await _context.Set<CabinType>()
            .FirstOrDefaultAsync(x => x.Name == CabinTypesName.Create(name));
    }

    public async Task<IEnumerable<CabinType>> GetAllAsync()
    {
        return await _context.Set<CabinType>().ToListAsync();
    }

    public async Task SaveAsync(CabinType cabinType)
    {
        await _context.Set<CabinType>().AddAsync(cabinType);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CabinType cabinType)
    {
        _context.Set<CabinType>().Update(cabinType);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CabinTypesId id)
    {
        var cabinType = await GetByIdAsync(id);
        if (cabinType != null)
        {
            _context.Set<CabinType>().Remove(cabinType);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteByNameAsync(string name)
    {
        var cabinType = await GetByNameStringAsync(name);
        if (cabinType != null)
        {
            _context.Set<CabinType>().Remove(cabinType);
            await _context.SaveChangesAsync();
        }
    }
}