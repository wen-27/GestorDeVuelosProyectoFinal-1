using System.Collections.Generic; 
using System.Threading.Tasks;    
using GestorDeVuelosProyectoFinal.Moduls.CardIssuers.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.CardIssuers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.CardIssuers.Domain.Repositories;

public interface ICardIssuersRepository
{
    Task<CardIssuer?> GetByIdAsync(CardIssuersId id);
    Task<IEnumerable<CardIssuer>> GetAllAsync();
    Task SaveAsync(CardIssuer cardIssuer);
    Task DeleteAsync(CardIssuersId id);
}