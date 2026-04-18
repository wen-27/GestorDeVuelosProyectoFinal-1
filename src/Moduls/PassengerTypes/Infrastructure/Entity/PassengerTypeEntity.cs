namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Infrastructure.Entity;

public sealed class PassengerTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
}
