using System;

namespace GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.ValueObject;

public sealed class DocumentTypesId 
{
    public Guid Value { get; }

    private DocumentTypesId(Guid value) => Value = value;

    public static DocumentTypesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del tipo de documento no es válido", nameof(value));

        return new DocumentTypesId(value);
    }
}