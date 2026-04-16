using System;
using GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Infrastructure.Entity;

namespace GestorDeVuelosProyectoFinal.src.Moduls.ReservationStateTransitions.Infrastructure.Entity;

public class ReservationStateTransitionsEntity
{
    public int Id { get; set; }

    public int From_Status_Id { get; set; }   // ✔
    public int To_Status_Id { get; set; }   
    public ReserveStatesEntity FromStatus { get; set; } = null!;
    public ReserveStatesEntity ToStatus { get; set; } = null!;
}
