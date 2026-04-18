using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinConfiguration.Infrastructure.Entity;

public class CabinConfiurationEntity
{
    public int Id { get; set; }
    public int AircraftId { get; set; }
    public int CabinTypeId { get; set; }
    public int RowStart { get; set; }
    public int RowEnd { get; set; }
    public int SeatsPerRow { get; set; }
    public string SeatLetters { get; set; } = string.Empty;
}