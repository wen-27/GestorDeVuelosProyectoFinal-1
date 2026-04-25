using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;

public sealed class GetFlightsWithReadyPassengersUseCase
{
    private readonly IBoardingPassesRepository _repository;

    public GetFlightsWithReadyPassengersUseCase(IBoardingPassesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadyBoardingFlightDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.FindFlightsWithReadyPassengersAsync();
    }
}
