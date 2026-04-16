using GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.ValueObject;
using Microsoft.VisualBasic;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.Aggregate;

public sealed class Customer
{
    // Propiedades con set privado para garantizar el encapsulamiento
    public CustomersId Id { get; private set; } = null!;
    public CustomersCreadoEn CreadoEn { get; private set; } = null!;

    // Constructor vacío para ORMs como Entity Framework (necesario a veces)
    private Customer() { }

    // Constructor principal privado
    private Customer(CustomersId id, CustomersCreadoEn creadoEn)
    {
        Id = id;
        CreadoEn = creadoEn;
    }

    public static Customer Create(Guid id, DateTime creadoEn)
    {
        return new Customer(
            CustomersId.Create(id),
            CustomersCreadoEn.Create(creadoEn)
        );
    }
}
