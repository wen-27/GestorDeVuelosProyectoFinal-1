using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;

public sealed class GetBoardingPassByTicketIdUseCase
{
    private readonly IBoardingPassesRepository _repository;

    public GetBoardingPassByTicketIdUseCase(IBoardingPassesRepository repository)
    {
        _repository = repository;
    }

    public async Task<BoardingPass?> ExecuteAsync(int ticketId, CancellationToken cancellationToken = default)
    {
        return await _repository.FindByTicketIdAsync(ticketId);
    }
}
