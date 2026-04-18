using System;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Infrastructure.Entity;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Entity;

public class AircraftModelsEntity
{
    public int Id { get; set; }
    public int AircraftManufacturerId { get; set; } //Fk
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public decimal? Weight { get; set; }
    public decimal? FuelConsumption { get; set; }
    public int? CruiseSpeed { get; set; }
    public int? CruiseAltitude { get; set; }
    public AircraftManufacturerEntity? Manufacturer { get; set; }
}
