using System;
using System.Collections.Generic;
using System.Text;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;

namespace UniParche.Domain.Interfaces.Services;

/// <summary>
/// Servicio específico para manejar operaciones de negocio de usuarios
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Obtiene todos los usuarios de forma asincrónica
    /// </summary>
    Task<IEnumerable<User>> GetAllUsersAsync();

    /// <summary>
    /// Obtiene un usuario por su identificador
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<User?> GetUserByIdAsync(int userId);

    /// <summary>
    /// Obtiene un usuario por su nombre de usuario
    /// </summary>
    /// <param name="userName">Nombre de usuario</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<User?> GetUserByUserNameAsync(string userName);

    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    /// <param name="email">Email del usuario</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<User?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Obtiene un usuario con toda su información relacionada
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Usuario con detalles completos</returns>
    Task<User?> GetUserWithDetailsAsync(int userId);

    /// <summary>
    /// Obtiene todos los usuarios de una universidad específica
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Colección de usuarios de la universidad</returns>
    Task<IEnumerable<User>> GetUsersByUniversityAsync(int universityId);

    /// <summary>
    /// Obtiene usuarios de una universidad con paginación
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="pageNumber">Número de página (comienza en 1)</param>
    /// <param name="pageSize">Cantidad de registros por página</param>
    /// <returns>Usuarios paginados</returns>
    Task<(IEnumerable<User> users, int totalCount)> GetUsersByUniversityPaginatedAsync(int universityId, int pageNumber, int pageSize);

    /// <summary>
    /// Obtiene usuarios por carrera dentro de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="carrerName">Nombre de la carrera</param>
    /// <returns>Usuarios de la carrera</returns>
    Task<IEnumerable<User>> GetUsersByCarrerAsync(int universityId, string carrerName);

    /// <summary>
    /// Obtiene usuarios por semestre dentro de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="semester">Número del semestre</param>
    /// <returns>Usuarios del semestre</returns>
    Task<IEnumerable<User>> GetUsersBySemesterAsync(int universityId, int semester);

    /// <summary>
    /// Busca usuarios por nombre o nombre de usuario
    /// </summary>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Usuarios que coinciden</returns>
    Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);

    /// <summary>
    /// Busca usuarios dentro de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="searchTerm">Palabra clave a buscar</param>
    /// <returns>Usuarios de la universidad que coinciden</returns>
    Task<IEnumerable<User>> SearchUsersByUniversityAsync(int universityId, string searchTerm);

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    /// <param name="user">Datos del usuario a crear</param>
    /// <returns>Usuario creado con su ID asignado</returns>
    Task<User> CreateUserAsync(User user);

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    /// <param name="user">Datos del usuario a actualizar</param>
    /// <returns>Usuario actualizado</returns>
    Task<User> UpdateUserAsync(User user);

    /// <summary>
    /// Actualiza la foto de perfil de un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="profilePictureUrl">URL de la nueva foto de perfil</param>
    /// <returns>True si se actualizó correctamente</returns>
    Task<bool> UpdateProfilePictureAsync(int userId, string profilePictureUrl);

    /// <summary>
    /// Actualiza la contraseña de un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <param name="passwordHash">Hash de la nueva contraseña</param>
    /// <returns>True si se actualizó correctamente</returns>
    Task<bool> UpdatePasswordAsync(int userId, string passwordHash);

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario a eliminar</param>
    /// <returns>True si se eliminó correctamente</returns>
    Task<bool> DeleteUserAsync(int userId);

    /// <summary>
    /// Obtiene los usuarios más activos de una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Cantidad de usuarios a obtener</param>
    /// <returns>Usuarios ordenados por actividad</returns>
    Task<IEnumerable<User>> GetMostActiveUsersAsync(int universityId, int take);

    /// <summary>
    /// Obtiene usuarios registrados recientemente en una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <param name="take">Cantidad de usuarios a obtener</param>
    /// <returns>Usuarios ordenados por fecha de registro reciente</returns>
    Task<IEnumerable<User>> GetRecentlyRegisteredUsersAsync(int universityId, int take);

    /// <summary>
    /// Obtiene sugerencias de usuarios a seguir en la misma universidad
    /// </summary>
    /// <param name="userId">Identificador del usuario actual</param>
    /// <param name="take">Cantidad de sugerencias</param>
    /// <returns>Usuarios sugeridos</returns>
    Task<IEnumerable<User>> GetSuggestedUsersAsync(int userId, int take);

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
    /// Obtiene estadísticas de un usuario
    /// </summary>
    /// <param name="userId">Identificador del usuario</param>
    /// <returns>Estadísticas del usuario (posts, comentarios, likes)</returns>
    Task<UserStatistics?> GetUserStatisticsAsync(int userId);

    /// <summary>
    /// Cuenta el total de usuarios en una universidad
    /// </summary>
    /// <param name="universityId">Identificador de la universidad</param>
    /// <returns>Número total de usuarios</returns>
    Task<int> CountUsersByUniversityAsync(int universityId);
}
