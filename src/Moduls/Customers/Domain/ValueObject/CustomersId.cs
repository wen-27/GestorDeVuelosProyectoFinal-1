using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.ValueObject;

public class CustomersId
{
    public  Guid Value { get; }
    private CustomersId(Guid value) => Value = value;

    public static CustomersId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del cliente no es válido", nameof(value));

        return new CustomersId(value);
    }
}
