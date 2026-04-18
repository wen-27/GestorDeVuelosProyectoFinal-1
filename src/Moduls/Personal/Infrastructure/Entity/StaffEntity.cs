namespace GestorDeVuelosProyectoFinal.Moduls.Personal.Infrastructure.Persistence.Entities;

public sealed class StaffEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int PositionId { get; set; }
    public int? AirlineId { get; set; }
    public int? AirportId { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
