using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMediumTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMediumTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentMediumTypes.Domain.Repositories;

public interface IPaymentMediumTypesRepository
{
    Task<Aggregate.PaymentMediumTypes?> GetByIdAsync(PaymentMediumTypesId id);
    Task<IEnumerable<Aggregate.PaymentMediumTypes>> GetAllAsync();
    Task SaveAsync(Aggregate.PaymentMediumTypes paymentMediumTypes);
    Task DeleteAsync(PaymentMediumTypesId id);
}
