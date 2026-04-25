namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

public sealed class BoardingPassBoardingAt
{
    public DateTime Value { get; }

    private BoardingPassBoardingAt(DateTime value) => Value = value;

    public static BoardingPassBoardingAt Create(DateTime value)
    {
        if (value == default)
            throw new ArgumentException("La hora de abordaje es obligatoria.", nameof(value));

        return new BoardingPassBoardingAt(value);
    }
}
