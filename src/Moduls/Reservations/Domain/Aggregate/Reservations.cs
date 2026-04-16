using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.Aggregate;

public sealed class Reservations
{
    // Propiedades con set privado para garantizar el encapsulamiento
    public ReverseId Id { get; private set; } = null!;
    public ReservationDate Date { get; private set; } = null!;
    public ReserveTotalAmount TotalAmount { get; private set; } = null!;
    public ReserveExpiresAt ExpiresAt { get; private set; } = null!;
    public ReserveCreateAt CreateAt { get; private set; } = null!;
    public ReserveUpdatedAt UpdatedAt { get; private set; } = null!;

    // Constructor vacío para ORMs como Entity Framework (necesario a veces)
    private Reservations() { }

    // Constructor principal privado
    private Reservations(
        ReverseId id,
        ReservationDate date,
        ReserveTotalAmount totalAmount,
        ReserveExpiresAt expiresAt,
        ReserveCreateAt createAt,
        ReserveUpdatedAt updatedAt)
    {
        Id = id;
        Date = date;
        TotalAmount = totalAmount;
        ExpiresAt = expiresAt;
        CreateAt = createAt;
        UpdatedAt = updatedAt;
    }

    public static Reservations Create(
        Guid id,
        DateTime date,
        decimal totalAmount,
        DateTime expiresAt,
        DateTime createAt,
        DateTime updatedAt)
    {
        return new Reservations(
            ReverseId.Create(id),
            ReservationDate.Create(date),
            ReserveTotalAmount.Create(totalAmount),
            ReserveExpiresAt.Create(expiresAt),
            ReserveCreateAt.Create(createAt),
            ReserveUpdatedAt.Create(updatedAt)
        );
    }

    public void UpdateDate(DateTime newDate)
    {
        // El Value Object ReservationDate se encarga de validar (fecha no vacia, fecha en el pasado, etc.)
        Date = ReservationDate.Create(newDate);
    }  
}
