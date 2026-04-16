using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.Repositories;

public interface PassengersRepository
{
    Task<Aggregate.Passengers?> GetByIdAsync(PassengersId id);
    Task<IEnumerable<Aggregate.Passengers>> GetAllAsync();
    Task SaveAsync(Aggregate.Passengers passengers);
    Task DeleteAsync(PassengersId id);
}
