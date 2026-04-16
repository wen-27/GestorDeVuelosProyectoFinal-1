using System;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.People.Domain.Aggregate;

public sealed class Person // <--- Cambiado a Person
{
    public PeopleId Id { get; private set; } = null!;
    public DocumentTypesId DocumentTypeId { get; private set; } = null!;
    public PeopleDocumentNumber DocumentNumber { get; private set; } = null!;
    public PeopleNames Names { get; private set; } = null!;
    public PeopleLastNames LastNames { get; private set; } = null!;
    public PeopleBirthDate BirthDate { get; private set; } = null!;
    public PeopleGender Gender { get; private set; } = null!;
    public AddressesId? AddressId { get; private set; }

    private Person() { }

    public static Person Create(
        Guid id, 
        Guid documentTypeId, 
        string documentNumber, 
        string names, 
        string lastNames, 
        DateTime? birthDate, 
        char? gender, 
        Guid? addressId)
    {
        return new Person // <--- Cambiado a new Person
        {
            Id = PeopleId.Create(id),
            DocumentTypeId = DocumentTypesId.Create(documentTypeId),
            DocumentNumber = PeopleDocumentNumber.Create(documentNumber),
            Names = PeopleNames.Create(names),
            LastNames = PeopleLastNames.Create(lastNames),
            BirthDate = PeopleBirthDate.Create(birthDate),
            Gender = PeopleGender.Create(gender),
            AddressId = addressId.HasValue ? AddressesId.Create(addressId.Value) : null
        };
    }
}