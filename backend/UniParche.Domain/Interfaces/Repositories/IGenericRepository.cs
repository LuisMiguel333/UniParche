using System.Linq.Expressions;
using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : AuditBase
{
    // ========== Operaciones de Lectura ==========

    /// <summary>
    /// Obtiene todos los registros de forma asincrónica
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Obtiene un registro por su identificador
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Obtiene un registro por su nombre
    /// </summary>
    Task<T?> GetByNameAsync(string name);

    /// <summary>
    /// Obtiene registros que cumplan con un predicado específico
    /// </summary>
    Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Obtiene el primer registro que cumple con la condición especificada
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    // ========== Operaciones de Creación ==========

    /// <summary>
    /// Agrega una nueva entidad a la base de datos
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Agrega múltiples entidades a la base de datos
    /// </summary>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    // ========== Operaciones de Actualización ==========

    /// <summary>
    /// Actualiza una entidad existente
    /// </summary>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Actualiza múltiples entidades existentes
    /// </summary>
    Task UpdateRangeAsync(IEnumerable<T> entities);

    // ========== Operaciones de Eliminación ==========

    /// <summary>
    /// Elimina un registro por su identificador
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Elimina una entidad específica
    /// </summary>
    Task<bool> DeleteAsync(T entity);

    /// <summary>
    /// Elimina múltiples entidades
    /// </summary>
    Task<bool> DeleteRangeAsync(IEnumerable<T> entities);

    // ========== Otras Operaciones ==========

    /// <summary>
    /// Cuenta el total de registros en la tabla
    /// </summary>
    Task<int> CountAsync();

    /// <summary>
    /// Verifica si existe un registro que cumpla con la condición
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}
