using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinTypes.Application.UseCases;

public class DeleteCabinTypeUseCase
{
    private readonly ICabinTypesRepository _repository;

    public DeleteCabinTypeUseCase(ICabinTypesRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(int id)
    {
        var cabinId = CabinTypesId.Create(id);
        
        // Opcional: Validar si existe antes de intentar borrar
        var existing = await _repository.GetByIdAsync(cabinId);
        if (existing == null)
            throw new InvalidOperationException($"No se encontró el tipo de cabina con ID {id}");

        await _repository.DeleteAsync(cabinId);
    }
}