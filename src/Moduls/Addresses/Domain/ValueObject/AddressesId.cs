using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;

public sealed record AddressesId
{
    public Guid Value { get; }
    public AddressesId(Guid value) => Value = value;

    public static AddressesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El ID de la dirección no puede estar vacío.", nameof(value));

        return new AddressesId(value);
    }
    public override string ToString() => Value.ToString();
}