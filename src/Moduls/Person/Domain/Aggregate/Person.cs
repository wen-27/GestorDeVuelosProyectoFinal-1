using System;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.People.Domain.Aggregate;

public sealed class Person
{
    public PeopleId Id { get; private set; } = null!;
    public DocumentTypesId DocumentTypeId { get; private set; } = null!;
    public PeopleDocumentNumber DocumentNumber { get; private set; } = null!;
    public PeopleNames FirstName { get; private set; } = null!;
    public PeopleLastNames LastNames { get; private set; } = null!;
    public PeopleBirthDate BirthDate { get; private set; } = null!;
    public PeopleGender Gender { get; private set; } = null!;
    public AddressesId? AddressId { get; private set; }
    public PersonCreatedAt CreatedAt { get; private set; } = null!;
    public PersonUpdatedAt UpdatedAt { get; private set; } = null!;

    private Person() { }

    private Person(
        PeopleId id,
        DocumentTypesId documentTypeId,
        PeopleDocumentNumber documentNumber,
        PeopleNames firstName,
        PeopleLastNames lastNames,
        PeopleBirthDate birthDate,
        PeopleGender gender,
        AddressesId? addressId,
        PersonCreatedAt createdAt,
        PersonUpdatedAt updatedAt)
    {
        Id = id;
        DocumentTypeId = documentTypeId;
        DocumentNumber = documentNumber;
        FirstName = firstName;
        LastNames = lastNames;
        BirthDate = birthDate;
        Gender = gender;
        AddressId = addressId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Person Create(
        int documentTypeId, 
        string documentNumber, 
        string firstName, 
        string lastNames, 
        DateTime? birthDate, 
        char? gender, 
        int? addressId,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new Person(
            id: null!,
            documentTypeId: DocumentTypesId.Create(documentTypeId),
            documentNumber: PeopleDocumentNumber.Create(documentNumber),
            firstName: PeopleNames.Create(firstName),
            lastNames: PeopleLastNames.Create(lastNames),
            birthDate: PeopleBirthDate.Create(birthDate),
            gender: PeopleGender.Create(gender),
            addressId: addressId.HasValue ? AddressesId.Create(addressId.Value) : null,
            createdAt: PersonCreatedAt.Create(createdAt),
            updatedAt: PersonUpdatedAt.Create(updatedAt)
        );
    }

    public static Person FromPrimitives(
        int id,
        int documentTypeId,
        string documentNumber,
        string firstName,
        string lastName,
        DateTime? birthDate,
        char? gender,
        int? addressId,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new Person(
            PeopleId.Create(id),
            DocumentTypesId.Create(documentTypeId),
            PeopleDocumentNumber.Create(documentNumber),
            PeopleNames.Create(firstName),
            PeopleLastNames.Create(lastName),
            PeopleBirthDate.Create(birthDate),
            PeopleGender.Create(gender),
            addressId.HasValue ? AddressesId.Create(addressId.Value) : null,
            PersonCreatedAt.Create(createdAt),
            PersonUpdatedAt.Create(updatedAt)
        );
    }

    internal void SetId(int id)
    {
        Id = PeopleId.Create(id);
    }

    public void Update(
        int documentTypeId,
        string documentNumber,
        string firstName,
        string lastName,
        DateTime? birthDate,
        char? gender,
        int? addressId,
        DateTime updatedAt)
    {
        DocumentTypeId = DocumentTypesId.Create(documentTypeId);
        DocumentNumber = PeopleDocumentNumber.Create(documentNumber);
        FirstName = PeopleNames.Create(firstName);
        LastNames = PeopleLastNames.Create(lastName);
        BirthDate = PeopleBirthDate.Create(birthDate);
        Gender = PeopleGender.Create(gender);
        AddressId = addressId.HasValue ? AddressesId.Create(addressId.Value) : null;
        UpdatedAt = PersonUpdatedAt.Create(updatedAt);
    }
}
