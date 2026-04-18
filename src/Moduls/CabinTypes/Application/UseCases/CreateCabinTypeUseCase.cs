namespace GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Application.UseCases;

using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Aggregate;

public sealed class CreateCabinTypeUseCase
{
    private readonly ICabinTypesRepository _repository;

    public CreateCabinTypeUseCase(ICabinTypesRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(string name)
    {
        // Validar si ya existe
        var existing = await _repository.GetByNameStringAsync(name);
        if (existing != null)
            throw new Exception($"El tipo de cabina '{name}' ya existe.");

        // Crear la entidad (El Id será asignado por la BD al guardar)
        var cabinType = CabinType.Create(name);

        await _repository.SaveAsync(cabinType);
    }
}