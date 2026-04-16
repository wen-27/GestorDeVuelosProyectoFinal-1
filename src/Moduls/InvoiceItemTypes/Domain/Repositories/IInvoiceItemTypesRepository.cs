using GestorDeVuelosProyectoFinal.Moduls.InvoiceItemTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.InvoiceItemTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.InvoiceItemTypes.Domain.Repositories;

public interface IInvoiceItemTypesRepository
{
    Task<InvoiceItemType?> GetByIdAsync(InvoiceItemTypesId id);
    Task<IEnumerable<InvoiceItemType>> GetAllAsync();

    Task GetByNameAsync(InvoiceItemTypesName name);
    Task CreateAsync(InvoiceItemType invoiceItemType);
    Task UpdateAsync(InvoiceItemType invoiceItemType);
    Task SaveAsync(InvoiceItemType invoiceItemType);
    Task DeleteAsync(InvoiceItemTypesId id);

    /*
    Task GetByNameAsync(InvoiceItemTypesName name);
    Task DeactivateAsync(InvoiceItemTypesId id);
    Task ActivateAsync(InvoiceItemTypesId id);

    */
}
