using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.src.Moduls.Users.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Users.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Users.Domain.Repositories;

public interface IUsersRepository
{
    Task<User?> GetByIdAsync(UsersId id);
    Task<User?> GetByUsernameAsync(UsersUsername username); // Importante para el Login
    Task SaveAsync(User user);
    Task DeleteAsync(UsersId id);
}