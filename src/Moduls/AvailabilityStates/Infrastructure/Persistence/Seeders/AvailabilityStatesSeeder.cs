using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Infrastructure.Persistence.Seeders;

public sealed class AvailabilityStatesSeeder
{
    private readonly AppDbContext _context;

    private static readonly string[] _states =
    {
        "Available",
        "Assigned",
        "Vacation",
        "Leave",
        "Inactive"
    };

    public AvailabilityStatesSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        var existing = await _context.AvailabilityStates.Select(x => x.Name).ToHashSetAsync(StringComparer.OrdinalIgnoreCase);

        foreach (var name in _states)
        {
            if (existing.Contains(name))
                continue;

            await _context.AvailabilityStates.AddAsync(new AvailabilityStateEntity
            {
                Name = name
            });
        }

        await _context.SaveChangesAsync();
    }
}
