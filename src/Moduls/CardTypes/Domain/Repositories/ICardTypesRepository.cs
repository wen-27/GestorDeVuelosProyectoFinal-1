using System;
using GestorDeVuelosProyectoFinal.src.Moduls.CardTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.CardTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CardTypes.Domain.Repositories;

public interface ICardTypesRepository
{
    Task<Aggregate.CardTypes?> GetByIdAsync(CardTypesId id);
    Task<IEnumerable<Aggregate.CardTypes>> GetAllAsync();
    Task SaveAsync(Aggregate.CardTypes cardTypes);
    Task DeleteAsync(CardTypesId id);
}
