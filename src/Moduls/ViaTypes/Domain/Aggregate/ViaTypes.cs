using GestorDeVuelosProyectoFinal.Moduls.ViaTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.ViaTypes.Domain.Aggregate;

public sealed class ViaType
{
    public ViaTypesId Id { get; private set; } = null!;
    public ViaTypesName Name { get; private set; } = null!;

    private ViaType() { }

    private ViaType(ViaTypesId id, ViaTypesName name)
    {
        Id = id;
        Name = name;
    }

    public static ViaType Create(Guid id, string name)
    {
        return new ViaType(
            ViaTypesId.Create(id),
            ViaTypesName.Create(name)
        );
    }

    public void UpdateName(string newName)
    {
        Name = ViaTypesName.Create(newName);
    }
}