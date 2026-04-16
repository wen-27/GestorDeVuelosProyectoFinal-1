using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Domain.ValueObject;

public sealed record PhoneCodesId
{
    public Guid Value { get; }
    private PhoneCodesId(Guid value) => Value = value;

    public static PhoneCodesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El ID del código telefónico no puede estar vacío.");

        return new PhoneCodesId(value);
    }
}