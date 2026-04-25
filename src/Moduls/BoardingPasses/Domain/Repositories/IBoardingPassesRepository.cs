using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;

public interface IBoardingPassesRepository
{
    Task<BoardingPass?> FindByIdAsync(BoardingPassId id);
    Task<IEnumerable<BoardingPass>> FindAllAsync();
    Task<BoardingPass?> FindByCheckinIdAsync(int checkinId);
    Task<BoardingPass?> FindByTicketIdAsync(int ticketId);
    Task<BoardingPass?> FindByTicketCodeAsync(string ticketCode);
    Task<BoardingPass?> FindByBoardingPassCodeAsync(string boardingPassCode);
    Task<BoardingPass?> FindByDocumentAsync(string documentNumber);
    Task<IEnumerable<ReadyBoardingFlightDto>> FindFlightsWithReadyPassengersAsync();
    Task<IEnumerable<ReadyToBoardPassengerDto>> FindReadyToBoardByFlightAsync(int flightId);
    Task SaveAsync(BoardingPass boardingPass);
    Task UpdateAsync(BoardingPass boardingPass);
    Task DeleteAsync(BoardingPassId id);
}
