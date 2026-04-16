using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Users.Domain.ValueObject;

public sealed class UsersId
{
    public Guid Value { get; }
    private UsersId(Guid value) => Value = value;

    public static UsersId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del usuario no es válido.");
        return new UsersId(value);
    }
}