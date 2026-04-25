namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;

public sealed class ReadyToBoardPassengerDto
{
    public int FlightId { get; }
    public string FlightCode { get; }
    public string PassengerName { get; }
    public string PassengerDocument { get; }
    public string SeatCode { get; }
    public string TicketCode { get; }
    public string TicketState { get; }
    public DateTime CheckedInAt { get; }

    public ReadyToBoardPassengerDto(
        int flightId,
        string flightCode,
        string passengerName,
        string passengerDocument,
        string seatCode,
        string ticketCode,
        string ticketState,
        DateTime checkedInAt)
    {
        if (flightId <= 0)
            throw new ArgumentException("El vuelo debe ser valido.", nameof(flightId));
        if (string.IsNullOrWhiteSpace(flightCode))
            throw new ArgumentException("El codigo del vuelo es obligatorio.", nameof(flightCode));
        if (string.IsNullOrWhiteSpace(passengerName))
            throw new ArgumentException("El nombre del pasajero es obligatorio.", nameof(passengerName));
        if (string.IsNullOrWhiteSpace(passengerDocument))
            throw new ArgumentException("El documento del pasajero es obligatorio.", nameof(passengerDocument));
        if (string.IsNullOrWhiteSpace(seatCode))
            throw new ArgumentException("El asiento es obligatorio.", nameof(seatCode));
        if (string.IsNullOrWhiteSpace(ticketCode))
            throw new ArgumentException("El codigo del tiquete es obligatorio.", nameof(ticketCode));
        if (string.IsNullOrWhiteSpace(ticketState))
            throw new ArgumentException("El estado del tiquete es obligatorio.", nameof(ticketState));

        FlightId = flightId;
        FlightCode = flightCode.Trim().ToUpperInvariant();
        PassengerName = passengerName.Trim();
        PassengerDocument = passengerDocument.Trim();
        SeatCode = seatCode.Trim().ToUpperInvariant();
        TicketCode = ticketCode.Trim();
        TicketState = ticketState.Trim();
        CheckedInAt = checkedInAt;
    }
}
