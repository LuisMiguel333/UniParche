using UniParche.Domain.Entities;

namespace UniParche.Domain.Interfaces.Repositories;

/// <summary>
/// Repositorio específico para manejar operaciones de usuarios en la red social
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>
    /// Obtiene un usuario por su nombre de usuario
    /// </summary>
    /// <param name="userName">Nombre de usuario</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<User?> GetByUserNameAsync(string userName);

    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    /// <param name="email">Email del usuario</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Obtiene un usuario con toda su información relacionada (universidad, posts, comentarios)
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Usuario con información relacionada</returns>
    Task<User?> GetUserWithDetailsAsync(int userId);

    /// <summary>
    /// Obtiene todos los usuarios de una universidad específica
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Colección de usuarios de la universidad</returns>
    Task<IEnumerable<User>> GetUsersByUniversityIdAsync(int universityId);

    /// <summary>
    /// Obtiene usuarios de una universidad con paginación
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="skip">Número de registros a saltar</param>
    /// <param name="take">Número de registros a obtener</param>
    /// <returns>Usuarios paginados de la universidad</returns>
    Task<IEnumerable<User>> GetUsersByUniversityWithPaginationAsync(int universityId, int skip, int take);

    /// <summary>
    /// Obtiene usuarios por carrera/programa dentro de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="carrerName">Nombre de la carrera</param>
    /// <returns>Colección de usuarios de la carrera</returns>
    Task<IEnumerable<User>> GetUsersByCarrerAsync(int universityId, string carrerName);

    /// <summary>
    /// Obtiene usuarios por semestre dentro de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="semester">Número del semestre</param>
    /// <returns>Colección de usuarios del semestre</returns>
    Task<IEnumerable<User>> GetUsersBySemesterAsync(int universityId, int semester);

    /// <summary>
    /// Busca usuarios por nombre o nombre de usuario
    /// </summary>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Usuarios que coinciden con la búsqueda</returns>
    Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);

    /// <summary>
    /// Busca usuarios dentro de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Usuarios de la universidad que coinciden con la búsqueda</returns>
    Task<IEnumerable<User>> SearchUsersByUniversityAsync(int universityId, string searchTerm);

    /// <summary>
    /// Verifica si existe un usuario con un nombre de usuario específico
    /// </summary>
    /// <param name="userName">Nombre de usuario</param>
    /// <returns>True si existe, False en caso contrario</returns>
    Task<bool> UserNameExistsAsync(string userName);

    /// <summary>
    /// Verifica si existe un usuario con un email específico
    /// </summary>
    /// <param name="email">Email del usuario</param>
    /// <returns>True si existe, False en caso contrario</returns>
    Task<bool> EmailExistsAsync(string email);

    /// <summary>
    /// Obtiene los usuarios más activos de una universidad (con más posts o comentarios)
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Número de usuarios a obtener</param>
    /// <returns>Usuarios ordenados por actividad</returns>
    Task<IEnumerable<User>> GetMostActiveUsersAsync(int universityId, int take);

    /// <summary>
    /// Obtiene usuarios registrados recientemente en una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Número de usuarios a obtener</param>
    /// <returns>Usuarios ordenados por fecha de registro más reciente</returns>
    Task<IEnumerable<User>> GetRecentlyRegisteredUsersAsync(int universityId, int take);

    /// <summary>
    /// Cuenta el total de usuarios en una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Número total de usuarios</returns>
    Task<int> CountUsersByUniversityAsync(int universityId);

    /// <summary>
    /// Cuenta el total de usuarios en una carrera específica
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="carrerName">Nombre de la carrera</param>
    /// <returns>Número de usuarios en la carrera</returns>
    Task<int> CountUsersByCarrerAsync(int universityId, string carrerName);

    /// <summary>
    /// Obtiene estadísticas de un usuario (posts, comentarios, likes realizados)
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Objeto con estadísticas del usuario</returns>
    Task<UserStatistics?> GetUserStatisticsAsync(int userId);

    /// <summary>
    /// Obtiene usuarios suggeridos para seguir (de la misma universidad)
    /// </summary>
    /// <param name="userId">Identificador del usuario actual</param>
    /// <param name="take">Número de sugerencias a obtener</param>
    /// <returns>Usuarios sugeridos de la misma universidad</returns>
    Task<IEnumerable<User>> GetSuggestedUsersAsync(int userId, int take);
}

/// <summary>
/// Clase para almacenar estadísticas de un usuario
/// </summary>
public class UserStatistics
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int TotalPosts { get; set; }
    public int TotalComments { get; set; }
    public int TotalLikesReceived { get; set; }
    public int TotalLikesGiven { get; set; }
    public DateTime RegisterDate { get; set; }
}
