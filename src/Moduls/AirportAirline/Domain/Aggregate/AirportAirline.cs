using GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.Aggregate;

public sealed class AirportAirline
{
    // Propiedades con set privado para garantizar el encapsulamiento
    public AirportAirlineId Id { get; private set; } = null!;
    public AirportAirlineStartDate StartDate { get; private set; } = null!;
    public AirpotAirlinesEndDate EndDate { get; private set; } = null!;
    public AirportAirlineTerminal Terminal { get; private set; } = null!;
    public AirportAirlineActive Active { get; private set; } = null!;

    // Constructor vacío para ORMs como Entity Framework (necesario a veces)
    private AirportAirline() { }

    // Constructor principal privado
    private AirportAirline(AirportAirlineId id, AirportAirlineStartDate startDate, AirpotAirlinesEndDate endDate, AirportAirlineTerminal terminal, AirportAirlineActive active)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        Terminal = terminal;
        Active = active;
    }

    public static AirportAirline Create(Guid id, DateTime startDate, DateTime endDate, string terminal, bool active)
    {
        return new AirportAirline(
            AirportAirlineId.Create(id),
            AirportAirlineStartDate.Create(startDate),
            AirpotAirlinesEndDate.Create(endDate),
            AirportAirlineTerminal.Create(terminal),
            AirportAirlineActive.Create(active)
        );
    }
}