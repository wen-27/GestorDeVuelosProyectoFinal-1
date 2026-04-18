using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.UseCases;

public sealed class CreateAircraftUseCase
{
    private readonly IAircraftRepository _aircraftRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAircraftUseCase(IAircraftRepository aircraftRepository, IUnitOfWork unitOfWork)
    {
        _aircraftRepository = aircraftRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Aggregate.Aircraft> ExecuteAsync(
        int id,
        string aircraftRegistration, 
        DateTime dateManufactured, 
        bool isActive, 
        CancellationToken cancellationToken = default)
    {
        var aircraftId = AircraftId.Create(id);
        var existingAircraft = await _aircraftRepository.GetByIdAsync(aircraftId, cancellationToken);

        if (existingAircraft is not null)
        {
            throw new InvalidOperationException($"Avión con id '{aircraftId}' ya existe.");
        }

        var aircraft = Domain.Aggregate.Aircraft.Create(id, aircraftRegistration, dateManufactured, isActive);

        await _aircraftRepository.AddAsync(aircraft, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return aircraft;
    }
}