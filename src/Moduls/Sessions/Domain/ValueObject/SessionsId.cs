using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Sessions.Domain.ValueObject;

public sealed class SessionsId
{
    public Guid Value { get; }
    private SessionsId(Guid value) => Value = value;

    public static SessionsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la sesión no es válido.");
        return new SessionsId(value);
    }
}