using System;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMethods.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentMethods.Domain.Aggregate;

public sealed class PaymentMethods
{
    // Propiedades con set privado para garantizar el encapsulamiento
    public PaymentMethodsId Id { get; private set; } = null!;
    public PaymentMethodsDisplayName DisplayName { get; private set; } = null!;

    // Constructor vacío para ORMs como Entity Framework (necesario a veces)
    private PaymentMethods() { }

    // Constructor principal privado
    private PaymentMethods(
        PaymentMethodsId id,
        PaymentMethodsDisplayName displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public static PaymentMethods Create(
        Guid id,
        string displayName)
    {
        return new PaymentMethods(
            PaymentMethodsId.Create(id),
            PaymentMethodsDisplayName.Create(displayName)
        );
    }

    public void UpdateDisplayName(string newDisplayName)
    {
        // El Value Object PaymentMethodsDisplayName se encarga de validar (longitud, números, etc.)
        DisplayName = PaymentMethodsDisplayName.Create(newDisplayName);
    }
}
