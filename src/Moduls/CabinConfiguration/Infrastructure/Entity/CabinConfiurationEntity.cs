using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CabinTypes.Infrastructure.Entity;
namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinConfiguration.Infrastructure.Entity;

public class CabinConfiurationEntity
{
    public int Id { get; set; }
    public int AircraftId { get; set; }
    public int CabinTypeId { get; set; }
    public int RowStart { get; set; }
    public int RowEnd { get; set; }
    public int SeatsPerRow { get; set; }
    public string SeatLetters { get; set; } = string.Empty;

    public required AircraftEntity aircraft { get; set; }
    public required CabinTypeEntity cabin_type { get; set; }

}

// en aircraftentity poner:
// public ICollection<CabinConfiurationEntity> cabin_configurations { get; set; }

// en cabin_type_entity poner:
// public ICollection<CabinConfiurationEntity> cabin_configurations { get; set; }