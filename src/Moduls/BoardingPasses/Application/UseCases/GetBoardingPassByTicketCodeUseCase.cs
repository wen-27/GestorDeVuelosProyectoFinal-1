using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;

public sealed class GetBoardingPassByTicketCodeUseCase
{
    private readonly IBoardingPassesRepository _repository;

    public GetBoardingPassByTicketCodeUseCase(IBoardingPassesRepository repository)
    {
        _repository = repository;
    }

    public async Task<BoardingPass?> ExecuteAsync(string ticketCode, CancellationToken cancellationToken = default)
    {
        return await _repository.FindByTicketCodeAsync(ticketCode);
    }
}
