using GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;

public sealed class AircraftModel
{
    public AircraftModelId Id { get; private set; } = null!;
    public AircraftModelName ModelName { get; private set; } = null!;
    public AircraftModelMaxCapacity MaxCapacity { get; private set; } = null!;
    public AircraftModelWeight MaxTakeoffWeight { get; private set; } = null!;
    public AircraftModelFuelConsumption FuelConsumption { get; private set; } = null!;
    public AircraftModelCruiseSpeed CruiseSpeed { get; private set; } = null!;
    public AircraftModelCruiseAltitude CruiseAltitude { get; private set; } = null!;

    private AircraftModel() { }
    private AircraftModel(
        AircraftModelId id, 
        AircraftModelName name, 
        AircraftModelMaxCapacity maxCapacity, AircraftModelWeight maxTakeoffWeight, AircraftModelFuelConsumption fuelConsumption, AircraftModelCruiseSpeed cruiseSpeed, AircraftModelCruiseAltitude cruiseAltitude)
    {
        Id = id;
        ModelName = name;
        MaxCapacity = maxCapacity;
        MaxTakeoffWeight = maxTakeoffWeight;
        FuelConsumption = fuelConsumption;
        CruiseSpeed = cruiseSpeed;
        CruiseAltitude = cruiseAltitude;
    }

    public static AircraftModel Create(
        int id,
        string name,
        int maxCapacity,
        decimal? weight,
        decimal? fuelConsumption,
        int? cruiseSpeed,
        int? cruiseAltitude)
    {
        return new AircraftModel(
            AircraftModelId.Create(id),
            AircraftModelName.Create(name),
            AircraftModelMaxCapacity.Create(maxCapacity),
            AircraftModelWeight.Create(weight),
            AircraftModelFuelConsumption.Create(fuelConsumption),
            AircraftModelCruiseSpeed.Create(cruiseSpeed),
            AircraftModelCruiseAltitude.Create(cruiseAltitude)
        );
    }
    public void UpdateName(string name) =>
        ModelName = AircraftModelName.Create(name);

    public void UpdateMaxCapacity(int capacity) =>
        MaxCapacity = AircraftModelMaxCapacity.Create(capacity);
}