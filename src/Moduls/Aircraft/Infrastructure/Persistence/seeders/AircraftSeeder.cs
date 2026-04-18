using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;
using AircraftAggregate =GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate;


namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Persistence.seeders;

public sealed class AircraftSeeder
{
    private readonly AppDbContext _context;
    private readonly IAircraftRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AircraftSeeder(
        AppDbContext context,
        IAircraftRepository repository,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync()
    {
        var aircraftModelIds = await _context.AircraftModels
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(x => x.Id)
            .Take(4)
            .ToListAsync();

        // Este seder hacerlo cuando este la tabla airlines

        // Revisar cuando este la tabla airlines

        /*
        var airlinesids = await _context.Airlines
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(x => x.Id)
            .Take(4)
            .ToListAsync();
        */
        /*

        if (aircraftModelIds.Count == 0 || aircraftModelIds.Count == 0)
            return;

        foreach (var aircraftModelId in aircraftModelIds)
        {
            var startRow = 1;

            for (int index = 0; index < aircraftModelIds.Count; index++)
            {
                var airlinesId = airlinesIds[index];
                var duplicate = await _repository.GetByAircraftModelsAndAirlinesAsync(aircraftModelId, airlinesId);
                if (duplicate is not null)
                {
                    startRow += GetRowBlockSize(index);
                    continue;
                }

                var rowBlock = GetRowBlockSize(index);
                var seatsPerRow = GetSeatsPerRow(index);
                var seatLetters = GetSeatLetters(index);
                */
    }
}
