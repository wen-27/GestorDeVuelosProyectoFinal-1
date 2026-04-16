namespace GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.ValueObject;

public sealed record PersonEmailsIsPrimary // Antes decía PeopleIsPrincipal
{
    public bool Value { get; }
    private PersonEmailsIsPrimary(bool value) => Value = value;
    public static PersonEmailsIsPrimary Create(bool value) => new(value);
}