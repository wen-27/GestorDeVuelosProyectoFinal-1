using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;

public sealed class GetBoardingPassByDocumentUseCase
{
    private readonly IBoardingPassesRepository _repository;

    public GetBoardingPassByDocumentUseCase(IBoardingPassesRepository repository)
    {
        _repository = repository;
    }

    public async Task<BoardingPass?> ExecuteAsync(string documentNumber, CancellationToken cancellationToken = default)
    {
        return await _repository.FindByDocumentAsync(documentNumber);
    }
}
