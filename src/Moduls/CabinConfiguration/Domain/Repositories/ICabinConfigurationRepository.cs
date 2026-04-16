using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Repositories;

public interface ICabinConfigurationRepository
{
    Task<CabinConfigurationRecord?> GetByIdAsync(CabinConfigurationId id);
    
    // Cambiado a Guid para evitar el error de AircraftId no encontrado
    Task<IEnumerable<CabinConfigurationRecord>> GetByAircraftIdAsync(Guid aircraftId);

    Task<IEnumerable<CabinConfigurationRecord>> GetAllAsync();
    
    Task SaveAsync(CabinConfigurationRecord configuration);
    Task DeleteAsync(CabinConfigurationId id);
}