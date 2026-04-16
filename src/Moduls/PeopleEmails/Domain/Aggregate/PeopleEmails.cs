using System;
using GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.EmailDomains.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.Aggregate;

public sealed class PersonEmail
{
    public PersonEmailsId Id { get; private set; } = null!;
    public PeopleId PersonId { get; private set; } = null!;
    public PersonEmailsUser UserEmail { get; private set; } = null!;
    public EmailDomainsId EmailDomainId { get; private set; } = null!;
    public PersonEmailsIsPrimary IsPrimary { get; private set; } = null!;

    private PersonEmail() { }

    public static PersonEmail Create(Guid id, Guid personId, string userEmail, Guid emailDomainId, bool isPrimary)
    {
        return new PersonEmail
        {
            Id = PersonEmailsId.Create(id),
            PersonId = PeopleId.Create(personId),
            UserEmail = PersonEmailsUser.Create(userEmail),
            EmailDomainId = EmailDomainsId.Create(emailDomainId),
            IsPrimary = PersonEmailsIsPrimary.Create(isPrimary)
        };
    }
}