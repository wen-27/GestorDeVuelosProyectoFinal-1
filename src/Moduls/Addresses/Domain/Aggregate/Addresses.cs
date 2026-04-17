using System;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;


namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Aggregate;

public sealed class Address
{
    public AddressesId Id { get; private set; } = null!;
    public RoadTypeId RoadTypeId { get; set; } = null!; // Ajustar según tu módulo de tipos_via
    public AddressesNameVia NameVia { get; set; } = null!;
    public AddressesNumber Number { get; set; } = null!;
    public AddressesComplement Complement { get;  set; } = null!;
    public AddressesPostalCode PostalCode { get; private set; } = null!;
    public CityId CityId { get; private set; } = null!;

    public Address() { }

    public Address(
        AddressesId id, 
        RoadTypeId roadTypeId, 
        AddressesNameVia nameVia, 
        AddressesNumber number, 
        AddressesComplement complement, 
        AddressesPostalCode postalCode, 
        CityId cityId)
    {
        Id = id;
        RoadTypeId = roadTypeId;
        NameVia = nameVia;
        Number = number;
        Complement = complement;
        PostalCode = postalCode;
        CityId = cityId;
    }

    public static Address Create(
        int id, 
        int roadTypeId, 
        string nameVia, 
        string? number, 
        string? complement, 
        string? postalCode, 
        int cityId)
    {
        return new Address(
            AddressesId.Create(id),
            RoadTypeId.Create(roadTypeId),
            AddressesNameVia.Create(nameVia),
            AddressesNumber.Create(number),
            AddressesComplement.Create(complement),
            AddressesPostalCode.Create(postalCode),
            CityId.Create(cityId)
        );
    }
}