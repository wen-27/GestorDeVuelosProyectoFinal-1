using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;

public sealed class BoardingPass
{
    public BoardingPassId Id { get; private set; } = null!;
    public int CheckinId { get; private set; }
    public int TicketId { get; private set; }
    public int FlightId { get; private set; }
    public BoardingPassCode Code { get; private set; } = null!;
    public string TicketCode { get; private set; } = null!;
    public string PassengerName { get; private set; } = null!;
    public string PassengerDocument { get; private set; } = null!;
    public string FlightCode { get; private set; } = null!;
    public string RouteLabel { get; private set; } = null!;
    public BoardingPassSeatCode SeatCode { get; private set; } = null!;
    public BoardingPassGate Gate { get; private set; } = null!;
    public DateTime DepartureAt { get; private set; }
    public BoardingPassBoardingAt BoardingAt { get; private set; } = null!;
    public BoardingPassStatus Status { get; private set; } = null!;

    private BoardingPass() { }

    private BoardingPass(
        BoardingPassId id,
        int checkinId,
        int ticketId,
        int flightId,
        BoardingPassCode code,
        string ticketCode,
        string passengerName,
        string passengerDocument,
        string flightCode,
        string routeLabel,
        BoardingPassSeatCode seatCode,
        BoardingPassGate gate,
        DateTime departureAt,
        BoardingPassBoardingAt boardingAt,
        BoardingPassStatus status)
    {
        Id = id;
        CheckinId = checkinId;
        TicketId = ticketId;
        FlightId = flightId;
        Code = code;
        TicketCode = ticketCode;
        PassengerName = passengerName;
        PassengerDocument = passengerDocument;
        FlightCode = flightCode;
        RouteLabel = routeLabel;
        SeatCode = seatCode;
        Gate = gate;
        DepartureAt = departureAt;
        BoardingAt = boardingAt;
        Status = status;
    }

    public static BoardingPass Create(
        int id,
        int checkinId,
        int ticketId,
        int flightId,
        string code,
        string ticketCode,
        string passengerName,
        string passengerDocument,
        string flightCode,
        string routeLabel,
        string seatCode,
        string gate,
        DateTime departureAt,
        DateTime boardingAt,
        string status)
    {
        if (string.IsNullOrWhiteSpace(ticketCode))
            throw new ArgumentException("El codigo del tiquete es obligatorio.", nameof(ticketCode));
        if (string.IsNullOrWhiteSpace(passengerName))
            throw new ArgumentException("El nombre del pasajero es obligatorio.", nameof(passengerName));
        if (string.IsNullOrWhiteSpace(passengerDocument))
            throw new ArgumentException("El documento del pasajero es obligatorio.", nameof(passengerDocument));
        if (string.IsNullOrWhiteSpace(flightCode))
            throw new ArgumentException("El codigo del vuelo es obligatorio.", nameof(flightCode));
        if (string.IsNullOrWhiteSpace(routeLabel))
            throw new ArgumentException("La ruta del pase es obligatoria.", nameof(routeLabel));

        return new BoardingPass(
            BoardingPassId.Create(id),
            checkinId,
            ticketId,
            flightId,
            BoardingPassCode.Create(code),
            ticketCode.Trim(),
            passengerName.Trim(),
            passengerDocument.Trim(),
            flightCode.Trim().ToUpperInvariant(),
            routeLabel.Trim(),
            BoardingPassSeatCode.Create(seatCode),
            BoardingPassGate.Create(gate),
            departureAt,
            BoardingPassBoardingAt.Create(boardingAt),
            BoardingPassStatus.Create(status));
    }
}
