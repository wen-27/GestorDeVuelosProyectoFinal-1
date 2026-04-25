using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;

public sealed class CreateOrUpdateBoardingPassUseCase
{
    private readonly IBoardingPassesRepository _repository;

    public CreateOrUpdateBoardingPassUseCase(IBoardingPassesRepository repository)
    {
        _repository = repository;
    }

    public async Task<BoardingPass?> GetExistingByCheckinAsync(int checkinId, CancellationToken cancellationToken = default)
    {
        return await _repository.FindByCheckinIdAsync(checkinId);
    }
}
