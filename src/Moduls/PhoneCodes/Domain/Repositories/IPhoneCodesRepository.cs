using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Domain.Repositories;

public interface IPhoneCodesRepository
{
    // Obtener por ID único
    Task<PhoneCode?> GetByIdAsync(PhoneCodesId id);

    // Obtener por código de país (ej: "+57")
    Task<PhoneCode?> GetByCountryCodeAsync(PhoneCodesCountryCode code);

    // Listar todos los códigos (para llenar dropdowns en el frontend)
    Task<IEnumerable<PhoneCode>> GetAllAsync();

    // Guardar o actualizar
    Task SaveAsync(PhoneCode phoneCode);

    // Eliminar
    Task DeleteAsync(PhoneCodesId id);
}