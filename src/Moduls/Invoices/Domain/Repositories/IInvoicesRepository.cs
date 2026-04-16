using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.src.Moduls.Invoices.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Invoices.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Invoices.Domain.Repositories;

public interface IInvoicesRepository
{
    Task<Invoice?> GetByIdAsync(InvoicesId id);
    Task<Invoice?> GetByInvoiceNumberAsync(InvoicesNumber numeroFactura);
    
    Task<Invoice?> GetByReservationIdAsync(ReverseId reservaId);

    Task SaveAsync(Invoice invoice);
    Task DeleteAsync(InvoicesId id);
}