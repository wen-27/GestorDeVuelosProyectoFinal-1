using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;

public sealed class GetReadyToBoardByFlightUseCase
{
    private readonly IBoardingPassesRepository _repository;

    public GetReadyToBoardByFlightUseCase(IBoardingPassesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadyToBoardPassengerDto>> ExecuteAsync(int flightId, CancellationToken cancellationToken = default)
    {
        return await _repository.FindReadyToBoardByFlightAsync(flightId);
    }
}
