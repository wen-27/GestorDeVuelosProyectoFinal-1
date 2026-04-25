using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.Interfaces;

public interface IBoardingPassesService
{
    Task<BoardingPass> CreateOrUpdateFromCheckinAsync(int checkinId, CancellationToken cancellationToken = default);
    Task<BoardingPass?> GetByBoardingPassCodeAsync(string boardingPassCode, CancellationToken cancellationToken = default);
    Task<BoardingPass?> GetByTicketCodeAsync(string ticketCode, CancellationToken cancellationToken = default);
    Task<BoardingPass?> GetByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default);
    Task<BoardingPass?> GetByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReadyBoardingFlightDto>> GetFlightsWithReadyPassengersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReadyToBoardPassengerDto>> GetReadyToBoardByFlightAsync(int flightId, CancellationToken cancellationToken = default);
}
