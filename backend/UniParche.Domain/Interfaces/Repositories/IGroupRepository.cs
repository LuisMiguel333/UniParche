using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

public interface IGroupRepository : IGenericRepository<Group>
{
    // ========== Consultas Específicas de Grupos ==========

    /// <summary>
    /// Obtiene todos los grupos pertenecientes a una universidad específica
    /// </summary>
    Task<IEnumerable<Group>> GetByUniversityAsync(int universityId);

    /// <summary>
    /// Obtiene un grupo con su lista completa de miembros incluida
    /// </summary>
    Task<Group?> GetWithMembersAsync(int id);
}