using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.Repositories;

public interface IDocumentTypesRepository
{
    Task<DocumentType?> GetByIdAsync(DocumentTypesId id);
    Task<DocumentType?> GetByCodeAsync(DocumentTypesCode code);
    Task<IEnumerable<DocumentType>> GetAllAsync();
    Task SaveAsync(DocumentType documentType);
    Task DeleteAsync(DocumentTypesId id);
}