using System;
using GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Aggregate;

// Renombrado para evitar choque con el namespace
public sealed class CabinConfigurationRecord 
{
    public CabinConfigurationId Id { get; private set; } = null!;
    // He quitado el using de Aircrafts y lo manejo como Guid temporalmente 
    // si es que no tienes ese módulo creado aún.
    public Guid AircraftId { get; private set; } 
    public CabinTypesId CabinTypeId { get; private set; } = null!;
    public CabinRowRange RowRange { get; private set; } = null!;
    public CabinSeatsPerRow SeatsPerRow { get; private set; } = null!;
    public CabinSeatLetters SeatLetters { get; private set; } = null!;

    private CabinConfigurationRecord() { }

    public static CabinConfigurationRecord Create(
        Guid id,
        Guid aircraftId,
        int cabinTypeId,
        int startRow,
        int endRow,
        int seatsPerRow,
        string seatLetters)
    {
        var seatsVO = CabinSeatsPerRow.Create(seatsPerRow);

        return new CabinConfigurationRecord
        {
            Id = CabinConfigurationId.Create(id),
            AircraftId = aircraftId, // Usando Guid directo para que no falle el build
            CabinTypeId = CabinTypesId.Create(cabinTypeId),
            RowRange = CabinRowRange.Create(startRow, endRow),
            SeatsPerRow = seatsVO,
            SeatLetters = CabinSeatLetters.Create(seatLetters, seatsVO.Value)
        };
    }
}
