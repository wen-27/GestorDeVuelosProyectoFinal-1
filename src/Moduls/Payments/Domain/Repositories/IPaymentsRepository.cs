using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Payments.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Payments.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Payments.Domain.Repositories;

public interface IPaymentsRepository
{
    Task<Aggregate.Payments?> GetByIdAsync(PaymentsId id);
    Task<IEnumerable<Aggregate.Payments>> GetAllAsync();
    Task SaveAsync(Aggregate.Payments payments);
    Task DeleteAsync(PaymentsId id);
}
