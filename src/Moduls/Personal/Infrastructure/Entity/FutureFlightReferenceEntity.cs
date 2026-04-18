namespace GestorDeVuelosProyectoFinal.Moduls.Personal.Infrastructure.Persistence.Entities;

public sealed class FutureFlightReferenceEntity
{
    public int Id { get; set; }
    public int? AircraftId { get; set; }
    public DateTime DepartureTime { get; set; }
}
