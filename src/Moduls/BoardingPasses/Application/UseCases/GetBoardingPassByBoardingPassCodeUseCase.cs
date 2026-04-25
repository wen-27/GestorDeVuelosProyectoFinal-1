using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;

public sealed class GetBoardingPassByBoardingPassCodeUseCase
{
    private readonly IBoardingPassesRepository _repository;

    public GetBoardingPassByBoardingPassCodeUseCase(IBoardingPassesRepository repository)
    {
        _repository = repository;
    }

    public async Task<BoardingPass?> ExecuteAsync(string boardingPassCode, CancellationToken cancellationToken = default)
    {
        return await _repository.FindByBoardingPassCodeAsync(boardingPassCode);
    }
}
