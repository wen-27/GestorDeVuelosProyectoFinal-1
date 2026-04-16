using System;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMethods.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMethods.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentMethods.Domain.Repositories;

public interface IPaymentMethodsRepository
{
    Task<Aggregate.PaymentMethods?> GetByIdAsync(PaymentMethodsId id);
    Task<IEnumerable<Aggregate.PaymentMethods>> GetAllAsync();
    Task SaveAsync(Aggregate.PaymentMethods paymentMethods);    
    Task DeleteAsync(PaymentMethodsId id);
}
