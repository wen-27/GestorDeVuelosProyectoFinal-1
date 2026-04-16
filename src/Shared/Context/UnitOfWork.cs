using GestorDeVuelosProyectoFinal.src.Shared.Contracts;
namespace GestorDeVuelosProyectoFinal.src.Shared.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RollbackAsync()
    {
        _context.ChangeTracker.Clear();
    }
}