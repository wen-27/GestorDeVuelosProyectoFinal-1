using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.EmailDomains.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.EmailDomains.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.EmailDomains.Domain.Repositories;

public interface IEmailDomainRepository
{
    Task<EmailDomain?> GetByIdAsync(EmailDomainsId id);
    Task<EmailDomain?> GetByNameAsync(EmailDomainName name);
    Task<IEnumerable<EmailDomain>> GetAllAsync();
    Task SaveAsync(EmailDomain emailDomain);
    Task DeleteAsync(EmailDomainsId id);
}