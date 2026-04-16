using System;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMediumTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentMediumTypes.Domain.Aggregate;

public sealed class PaymentMediumTypes
{
    // Propiedades con set privado para garantizar el encapsulamiento
    public PaymentMediumTypesId Id { get; private set; } = null!;
    public PaymentMediumTypesName Name { get; private set; } = null!;

    // Constructor vacío para ORMs como Entity Framework (necesario a veces)
    private PaymentMediumTypes() { }

    // Constructor principal privado
    private PaymentMediumTypes(
        PaymentMediumTypesId id,
        PaymentMediumTypesName name)
    {
        Id = id;
        Name = name;
    }   

    public static PaymentMediumTypes Create(
        Guid id,
        string name)
    {
        return new PaymentMediumTypes(
            PaymentMediumTypesId.Create(id),
            PaymentMediumTypesName.Create(name)
        );
    }

    public void UpdateName(string newName)
    {
        // El Value Object PaymentMediumTypesName se encarga de validar (longitud, números, etc.)
        Name = PaymentMediumTypesName.Create(newName);
    }
}
