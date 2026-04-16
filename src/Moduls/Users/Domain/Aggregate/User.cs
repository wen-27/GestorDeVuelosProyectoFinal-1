using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Users.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Roles.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Users.Domain.Aggregate;

public sealed class User
{
    public UsersId Id { get; private set; } = null!;
    public UsersUsername Username { get; private set; } = null!;
    public UsersPasswordHash PasswordHash { get; private set; } = null!;
    public Guid? PersonaId { get; private set; } // Opcional
    public RolesId RolId { get; private set; } = null!;
    public UsersIsActive IsActive { get; private set; } = null!;
    public DateTime? LastAccess { get; private set; }

    private User() { }

    public static User Create(
        Guid id, 
        string username, 
        string passwordHash, 
        Guid rolId, 
        Guid? personaId = null,
        bool isActive = true)
    {
        return new User
        {
            Id = UsersId.Create(id),
            Username = UsersUsername.Create(username),
            PasswordHash = UsersPasswordHash.Create(passwordHash),
            RolId = RolesId.Create(rolId),
            PersonaId = personaId,
            IsActive = UsersIsActive.Create(isActive)
        };
    }
}