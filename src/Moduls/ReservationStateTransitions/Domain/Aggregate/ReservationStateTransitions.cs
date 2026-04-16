using System;
using GestorDeVuelosProyectoFinal.Moduls.ReservationStateTransitions.Domain.ValueObject;
// Importamos tu Value Object de estados
using GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.ReservationStateTransitions.Domain.Aggregate;

public sealed class ReservationStateTransition
{
    public ReservationStateTransitionsId Id { get; private set; } = null!;
    
    // Tus FKs usando tu clase ReserveStateId
    public ReserveStateId OriginStateId { get; private set; } = null!;
    public ReserveStateId DestinationStateId { get; private set; } = null!;

    private ReservationStateTransition() { }

    public static ReservationStateTransition Create(Guid id, Guid originId, Guid destinationId)
    {
        if (originId == destinationId)
            throw new ArgumentException("El estado de origen y el de destino no pueden ser iguales.");

        return new ReservationStateTransition
        {
            Id = ReservationStateTransitionsId.Create(id),
            OriginStateId = ReserveStateId.Create(originId),
            DestinationStateId = ReserveStateId.Create(destinationId)
        };
    }
}