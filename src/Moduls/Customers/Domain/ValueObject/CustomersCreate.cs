using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.ValueObject;

public sealed class CustomersCreadoEn
{
    public DateTime Value { get; }
    private CustomersCreadoEn(DateTime value) => Value = value;

    public static CustomersCreadoEn Create(DateTime value)
    {
        if (value == DateTime.MinValue)
            throw new ArgumentException("El campo creado_en no puede ser igual a DateTime.MinValue");

        return new CustomersCreadoEn(value);
    }
}
