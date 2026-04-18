namespace GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Infrastructure.Persistence.Entities;

public sealed class StaffAvailabilityStateReferenceEntity
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int AvailabilityStateId { get; set; }
}
