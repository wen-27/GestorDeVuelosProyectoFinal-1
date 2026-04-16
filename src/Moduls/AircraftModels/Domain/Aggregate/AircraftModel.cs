using System;
using GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;
// AJUSTE AQUÍ: Usando tu namespace exacto (revisa si lleva el .src o no)
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.Aggregate;

public sealed class AircraftModel
{
    public AircraftModelId Id { get; private set; } = null!;

    public AircraftManufacturersId ManufacturerId { get; private set; } = null!; 
    public AircraftModelName ModelName { get; private set; } = null!;
    public AircraftModelMaxCapacity MaxCapacity { get; private set; } = null!;
    public AircraftModelWeight MaxTakeoffWeight { get; private set; } = null!;
    public AircraftModelFuelConsumption FuelConsumption { get; private set; } = null!;
    public AircraftModelCruiseSpeed CruiseSpeed { get; private set; } = null!;
    public AircraftModelCruiseAltitude CruiseAltitude { get; private set; } = null!;

    private AircraftModel() { }

    public static AircraftModel Create(
        Guid id,
        Guid manufacturerId,
        string modelName,
        int maxCapacity,
        decimal? weight,
        decimal? fuelConsumption,
        int? cruiseSpeed,
        int? cruiseAltitude)
    {
        return new AircraftModel
        {
            Id = AircraftModelId.Create(id),

            ManufacturerId = AircraftManufacturersId.Create(manufacturerId),
            ModelName = AircraftModelName.Create(modelName),
            MaxCapacity = AircraftModelMaxCapacity.Create(maxCapacity),
            MaxTakeoffWeight = AircraftModelWeight.Create(weight),
            FuelConsumption = AircraftModelFuelConsumption.Create(fuelConsumption),
            CruiseSpeed = AircraftModelCruiseSpeed.Create(cruiseSpeed),
            CruiseAltitude = AircraftModelCruiseAltitude.Create(cruiseAltitude)
        };
    }
}