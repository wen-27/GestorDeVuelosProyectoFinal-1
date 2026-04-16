using System;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentStatuses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentStatuses.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentStatuses.Domain.Repositories;
public interface IPaymentStatusesRepository
{
    Task<Aggregate.PaymentStatuses?> GetByIdAsync(PaymentStatuseId id);
    Task<IEnumerable<Aggregate.PaymentStatuses>> GetAllAsync();
    Task SaveAsync(Aggregate.PaymentStatuses paymentStatuses);
    Task DeleteAsync(PaymentStatuseId id);
}
