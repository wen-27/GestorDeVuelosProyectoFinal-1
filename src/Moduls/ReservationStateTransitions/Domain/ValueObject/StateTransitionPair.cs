using System;

namespace GestorDeVuelosProyectoFinal.Moduls.ReservationStateTransitions.Domain.ValueObject;

public sealed class StateTransitionPair
{
    public int FromStateId { get; }
    public int ToStateId { get; }

    private StateTransitionPair(int fromStateId, int toStateId)
    {
        FromStateId = fromStateId;
        ToStateId = toStateId;
    }

    public static StateTransitionPair Create(int fromStateId, int toStateId)
    {
        if (fromStateId <= 0 || toStateId <= 0)
            throw new ArgumentException("Los IDs de estado deben ser valores positivos.");

        if (fromStateId == toStateId)
            throw new ArgumentException("El estado de origen y el de destino no pueden ser iguales.");

        return new StateTransitionPair(fromStateId, toStateId);
    }
}