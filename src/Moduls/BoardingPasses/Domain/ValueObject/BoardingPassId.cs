namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

public sealed class BoardingPassId
{
    public int Value { get; }

    private BoardingPassId(int value) => Value = value;

    public static BoardingPassId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("El id del pase de abordar debe ser mayor que cero.", nameof(value));

        return new BoardingPassId(value);
    }
}
