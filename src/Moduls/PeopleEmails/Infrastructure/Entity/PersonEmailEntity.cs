namespace GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Infrastructure.Entity;

public sealed class PersonEmailEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string EmailUser { get; set; } = string.Empty;
    public int EmailDomainId { get; set; }
    public bool IsPrimary { get; set; }
}
