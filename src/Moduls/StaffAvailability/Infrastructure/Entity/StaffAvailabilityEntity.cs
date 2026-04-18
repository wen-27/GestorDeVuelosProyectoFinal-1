namespace GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Infrastructure.Entities;

public sealed class StaffAvailabilityEntity
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int AvailabilityStatusId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string? Notes { get; set; }
}