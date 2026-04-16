namespace GestorDeVuelosProyectoFinal.src.Shared.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync();
}