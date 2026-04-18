namespace GestorDeVuelosProyectoFinal.Moduls.People.Infrastructure.Persistence.Entities;

public sealed class PersonEntity
{
    public int Id { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public char? Gender { get; set; }
    public int? AddressId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
