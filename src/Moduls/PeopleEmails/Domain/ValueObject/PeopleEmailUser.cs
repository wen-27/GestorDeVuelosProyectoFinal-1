using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.ValueObject;

public sealed record PersonEmailsUser // Antes decía PeopleEmailUser
{
    public string Value { get; }
    private PersonEmailsUser(string value) => Value = value;

    public static PersonEmailsUser Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El usuario del email es obligatorio.");

        var trimmed = value.Trim().ToLower();
        if (trimmed.Contains("@"))
            throw new ArgumentException("El nombre de usuario no debe incluir el símbolo '@'.");

        return new PersonEmailsUser(trimmed);
    }
}