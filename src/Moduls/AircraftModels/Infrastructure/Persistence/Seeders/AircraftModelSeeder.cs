using GestorDeVuelosProyectoFinal.src.Shared.Contracts;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Persistence;

public sealed class AircraftModelSeeder
{
    private readonly IAircraftModelsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    private static readonly (string Name, int Capacity, int Speed)[] _seeds =
    [
        ("Boeing 737-800", 189, 842),
        ("Airbus A320",    180, 828),
        ("Boeing 787-9",   296, 903),
        ("Airbus A380",    555, 903),
        ("Embraer E190",   100, 870),
    ];

    public AircraftModelSeeder(IAircraftModelsRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var existing     = await _repository.FindAllAsync(cancellationToken);
        var existingNames = existing.Select(m => m.ModelName.Value)
                                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var (name, capacity, speed) in _seeds)
        {
            if (existingNames.Contains(name)) continue;

            // El id=0 solo funciona si tu BD autogenera el ID (identity/autoincrement).
            // Si el ID lo manejas tú, debes calcular el próximo disponible.
            var model = AircraftModel.Create(0, name, capacity, null, null, speed, null);
            await _repository.AddAsync(model, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}