using System;
using GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;


namespace GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Aggregate;

public sealed class CabinConfiguration 
{
    public CabinConfigurationId Id { get; private set; } = null!;
    public int AircraftId { get; private set; } 
    public CabinTypesId CabinTypeId { get; private set; } = null!;
    public CabinRowRange RowRange { get; private set; } = null!;
    public CabinSeatsPerRow SeatsPerRow { get; private set; } = null!;
    public CabinSeatLetters SeatLetters { get; private set; } = null!;

    private CabinConfiguration() { }

    public static CabinConfiguration Create(
        int aircraftId,
        int cabinTypeId,
        int startRow,
        int endRow,
        int seatsPerRow,
        string seatLetters)
    {
        var seatsVO = CabinSeatsPerRow.Create(seatsPerRow);

        return new CabinConfiguration
        {
            Id = CabinConfigurationId.Create(0), // 0 porque es autoincremental en BD
            AircraftId = aircraftId, // Usando Guid directo para que no falle el build
            CabinTypeId = CabinTypesId.Create(cabinTypeId),
            RowRange = CabinRowRange.Create(startRow, endRow),
            SeatsPerRow = seatsVO,
            SeatLetters = CabinSeatLetters.Create(seatLetters, seatsVO.Value)
        };
    }
    public static CabinConfiguration FromPrimitives(
        int id, int aircraftId, int cabinTypeId, int startRow, int endRow, int seatsPerRow, string seatLetters)
    {
        var config = Create(aircraftId, cabinTypeId, startRow, endRow, seatsPerRow, seatLetters);
        config.Id = CabinConfigurationId.Create(id);
        return config;
    }

    public void Update(
        int aircraftId,
        int cabinTypeId,
        int startRow,
        int endRow,
        int seatsPerRow,
        string seatLetters)
    {
        var seatsVO = CabinSeatsPerRow.Create(seatsPerRow);

        AircraftId = aircraftId;
        CabinTypeId = CabinTypesId.Create(cabinTypeId);
        RowRange = CabinRowRange.Create(startRow, endRow);
        SeatsPerRow = seatsVO;
        SeatLetters = CabinSeatLetters.Create(seatLetters, seatsVO.Value);
    }
}
            
