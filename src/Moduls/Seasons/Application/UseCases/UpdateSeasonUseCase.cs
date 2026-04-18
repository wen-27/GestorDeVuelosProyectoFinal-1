using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;

namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.Application.UseCases;

public sealed class UpdateSeasonUseCase
{
    private readonly ISeasonsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSeasonUseCase(ISeasonsRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, string name, string? description, decimal priceFactor)
    {
        var season = await _repository.GetByIdAsync(SeasonsId.Create(id))
            ?? throw new InvalidOperationException($"No se encontro la temporada con ID {id}.");

        var duplicate = await _repository.GetByNameAsync(SeasonsName.Create(name));
        if (duplicate is not null && duplicate.Id?.Value != id)
            throw new InvalidOperationException($"Ya existe otra temporada con nombre '{name}'.");

        season.Update(name, description, priceFactor);
        await _repository.UpdateAsync(season);
        await _unitOfWork.SaveChangesAsync();
    }
}
