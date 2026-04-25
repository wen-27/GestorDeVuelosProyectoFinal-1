using GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Customers.Application.UseCases;

public sealed class DeleteCustomerUseCase
{
    private readonly ICustomersRepository _repository;
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerUseCase(ICustomersRepository repository, AppDbContext context, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(CustomersId.Create(id))
            ?? throw new InvalidOperationException($"Customer with id '{id}' not found.");

        var hasBookings = await _context.Bookings
            .AsNoTracking()
            .AnyAsync(x => x.ClientId == customer.Id.Value);

        if (hasBookings)
            throw new InvalidOperationException("No se puede eliminar el cliente porque tiene reservas asociadas.");

        await _repository.DeleteAsync(customer.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
