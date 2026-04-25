namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;

public sealed class ReadyBoardingFlightDto
{
    public int FlightId { get; }
    public string FlightCode { get; }
    public string RouteLabel { get; }
    public DateTime DepartureAt { get; }

    public ReadyBoardingFlightDto(int flightId, string flightCode, string routeLabel, DateTime departureAt)
    {
        if (flightId <= 0)
            throw new ArgumentException("El vuelo debe ser valido.", nameof(flightId));
        if (string.IsNullOrWhiteSpace(flightCode))
            throw new ArgumentException("El codigo del vuelo es obligatorio.", nameof(flightCode));
        if (string.IsNullOrWhiteSpace(routeLabel))
            throw new ArgumentException("La ruta del vuelo es obligatoria.", nameof(routeLabel));

        FlightId = flightId;
        FlightCode = flightCode.Trim().ToUpperInvariant();
        RouteLabel = routeLabel.Trim();
        DepartureAt = departureAt;
    }
}
