namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.ValueObject;

public sealed record AirlinesIsActive
{
    public bool Value { get; }

    private AirlinesIsActive(bool value) => Value = value;

    public static AirlinesIsActive Create(bool value)
    {
        
        return new AirlinesIsActive(value);
    }
    
    public override string ToString() => Value ? "Activa" : "Inactiva";
}