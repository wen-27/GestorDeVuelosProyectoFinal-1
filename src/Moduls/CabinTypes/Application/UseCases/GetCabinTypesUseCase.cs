using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Application.UseCases;

public record CabinTypeResponse(int Id, string Name);

public sealed class GetCabinTypesUseCase
{
    private readonly ICabinTypesRepository _repository;

    public GetCabinTypesUseCase(ICabinTypesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CabinTypeResponse>> Execute()
    {
        var result = await _repository.GetAllAsync();
        
        return result.Select(c => new CabinTypeResponse(c.Id.Value, c.Name.Value));
    }
}