using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate;

public sealed class Aircraft
{
    public AircraftId Id { get; private set; } = null!;
    public AircraftRegistration Registration { get; private set; } = null!;

    public AircraftDateManufacture DateManufactured { get; private set; } = null!;

    public bool IsActive { get; private set; }

    private Aircraft() { }

    private Aircraft(AircraftId id, AircraftRegistration aircraftRegistration, AircraftDateManufacture dateManufactured, AircraftActive isActive)
    {
        Id = id;
        Registration = aircraftRegistration;
        DateManufactured = dateManufactured;
        IsActive = isActive.Value;
    }
    public static Aircraft Create(Guid id, string AircraftRegistration, DateTime dateManufactured, bool isActive)
    {
        return new Aircraft(
            AircraftId.Create(id),
            ValueObject.AircraftRegistration.Create(AircraftRegistration),
            AircraftDateManufacture.Create(dateManufactured),
            AircraftActive.Create(isActive)
        );
    }
    public void UpdateIsActive(bool newIsActive)
    {
        IsActive = newIsActive;
    }
}
