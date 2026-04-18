using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;
using CabinConfigurationAggregate = GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Aggregate.CabinConfiguration;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinConfiguration.Infrastructure.Persistence.seeders;

public sealed class CabinConfigurationSeeder
{
    private readonly AppDbContext _context;
    private readonly ICabinConfigurationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CabinConfigurationSeeder(
        AppDbContext context,
        ICabinConfigurationRepository repository,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync()
    {
        // Este seeder depende de tablas maestras:
        // 1. aircraft
        // 2. cabin_types
        // Si esas tablas no tienen datos, no sembramos nada para evitar violar FKs.
        var aircraftIds = await _context.Aircrafts
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(x => x.Id)
            .Take(3)
            .ToListAsync();

        var cabinTypeIds = await _context.CabinTypes
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(x => x.Id)
            .Take(4)
            .ToListAsync();

        if (aircraftIds.Count == 0 || cabinTypeIds.Count == 0)
            return;

        foreach (var aircraftId in aircraftIds)
        {
            var startRow = 1;

            for (int index = 0; index < cabinTypeIds.Count; index++)
            {
                var cabinTypeId = cabinTypeIds[index];
                var duplicate = await _repository.GetByAircraftAndCabinTypeAsync(aircraftId, cabinTypeId);
                if (duplicate is not null)
                {
                    startRow += GetRowBlockSize(index);
                    continue;
                }

                var rowBlock = GetRowBlockSize(index);
                var seatsPerRow = GetSeatsPerRow(index);
                var seatLetters = GetSeatLetters(index);

                var configuration = CabinConfigurationAggregate.Create(
                    aircraftId,
                    cabinTypeId,
                    startRow,
                    startRow + rowBlock - 1,
                    seatsPerRow,
                    seatLetters);

                await _repository.SaveAsync(configuration);
                startRow += rowBlock;
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private static int GetRowBlockSize(int index) => index switch
    {
        0 => 4,
        1 => 6,
        2 => 8,
        _ => 12
    };

    private static int GetSeatsPerRow(int index) => index switch
    {
        0 => 4,
        1 => 4,
        2 => 6,
        _ => 6
    };

    private static string GetSeatLetters(int index) => index switch
    {
        0 => "ACDF",
        1 => "ACDF",
        2 => "ABCDEF",
        _ => "ABCDEF"
    };
}
