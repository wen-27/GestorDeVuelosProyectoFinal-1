
namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Entity;

public class AircraftEntity
{
    public int Id { get; set; }
    public int AircraftModelId { get; set; }
    public int AirlinesId { get; set; }
    public string Registration { get; set; } = null!;
    public DateTime DateManufactured { get; set; }
    public bool IsActive { get; set; }
}
