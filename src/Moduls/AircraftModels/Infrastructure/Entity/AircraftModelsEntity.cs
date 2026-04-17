using System;

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

    // public required AircraftManufacturersEntity Manufacturer { get; set; } // PARA PONER FK, Pensiente tabla aircraft_manufacturers
}


// en aircraft_manufacturersentity poner:
// public ICollection<AircraftModelsEntity> Models { get; set; } = new List<AircraftModelsEntity>();

// solo eso yo ya lo agregue en configuration
