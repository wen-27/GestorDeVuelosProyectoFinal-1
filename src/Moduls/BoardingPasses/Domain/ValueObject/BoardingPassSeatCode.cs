using GestorDeVuelosProyectoFinal.src.Moduls.FlightSeats.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

public sealed class BoardingPassSeatCode
{
    public string Value { get; }

    private BoardingPassSeatCode(string value) => Value = value;

    public static BoardingPassSeatCode Create(string value)
    {
        var seatCode = FlightSeatsCode.Create(value);
        return new BoardingPassSeatCode(seatCode.Value);
    }

    public override string ToString() => Value;
}
