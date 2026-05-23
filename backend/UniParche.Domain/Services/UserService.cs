using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using UniParche.Domain.Entities;
using UniParche.Domain.Helpers;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;

namespace UniParche.Domain.Services;

/// <summary>
/// Implementación del servicio de usuarios con lógica de negocio
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserValidationHelper _validationHelper;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        UserValidationHelper validationHelper,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // ═══ Obtención de Usuarios ═══

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        _logger.LogInformation("Obteniendo todos los usuarios");
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        if (userId <= 0)
            return null;

        _logger.LogInformation("Obteniendo usuario con ID {UserId}", userId);
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return null;

        _logger.LogInformation("Buscando usuario por nombre: {UserName}", userName);
        return await _userRepository.GetByUserNameAsync(userName);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        _logger.LogInformation("Buscando usuario por email");
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User?> GetUserWithDetailsAsync(int userId)
    {
        var user = await _validationHelper.ValidateUserExistsAsync(userId);
        _logger.LogInformation("Obteniendo usuario con detalles: {UserId}", userId);
        return await _userRepository.GetUserWithDetailsAsync(userId);
    }

    public async Task<IEnumerable<User>> GetUsersByUniversityAsync(int universityId)
    {
        if (universityId <= 0)
            return Enumerable.Empty<User>();

        await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo usuarios de la universidad {UniversityId}", universityId);
        return await _userRepository.GetUsersByUniversityIdAsync(universityId);
    }

    public async Task<(IEnumerable<User> users, int totalCount)> GetUsersByUniversityPaginatedAsync(int universityId, int pageNumber, int pageSize)
    {
        UserValidationHelper.ValidatePagination(pageNumber, pageSize);
        await _validationHelper.ValidateUniversityExistsAsync(universityId);

        _logger.LogInformation("Obteniendo usuarios de universidad {UniversityId} - Página {Page}", universityId, pageNumber);

        var users = await _userRepository.GetUsersByUniversityIdAsync(universityId);
        var totalCount = users.Count();

        var paginatedUsers = users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paginatedUsers, totalCount);
    }

    public async Task<IEnumerable<User>> GetUsersByCarrerAsync(int universityId, string carrerName)
    {
        if (universityId <= 0 || string.IsNullOrWhiteSpace(carrerName))
            return Enumerable.Empty<User>();

        await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo usuarios de carrera {CarrerName}", carrerName);
        return await _userRepository.GetUsersByCarrerAsync(universityId, carrerName);
    }

    public async Task<IEnumerable<User>> GetUsersBySemesterAsync(int universityId, int semester)
    {
        if (universityId <= 0 || semester <= 0)
            return Enumerable.Empty<User>();

        await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo usuarios del semestre {Semester}", semester);
        return await _userRepository.GetUsersBySemesterAsync(universityId, semester);
    }

    public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
    {
        UserValidationHelper.ValidateSearchTerm(searchTerm);
        _logger.LogInformation("Buscando usuarios: {SearchTerm}", searchTerm);
        return await _userRepository.SearchUsersAsync(searchTerm);
    }

    public async Task<IEnumerable<User>> SearchUsersByUniversityAsync(int universityId, string searchTerm)
    {
        UserValidationHelper.ValidateSearchTerm(searchTerm);
        await _validationHelper.ValidateUniversityExistsAsync(universityId);

        _logger.LogInformation("Buscando usuarios en universidad {UniversityId}: {SearchTerm}", universityId, searchTerm);
        return await _userRepository.SearchUsersByUniversityAsync(universityId, searchTerm);
    }

    // ═══ Crear y Actualizar Usuario ═══

    public async Task<User> CreateUserAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        // Validaciones
        await _validationHelper.ValidateEmailAsync(user.email);
        await _validationHelper.ValidateUserNameAsync(user.user_name);
        await _validationHelper.ValidateUniversityExistsAsync(user.UniversityId);

        user.register_time = DateTime.UtcNow;

        _logger.LogInformation("Creando nuevo usuario: {Email}", user.email);
        return await _userRepository.AddAsync(user);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (user.Id <= 0)
            throw new ArgumentException("El usuario debe tener un ID válido", nameof(user.Id));

        var existingUser = await _validationHelper.ValidateUserExistsAsync(user.Id);

        // Validar email si cambió
        if (user.email != existingUser.email)
            await _validationHelper.ValidateEmailAsync(user.email, user.Id);

        _logger.LogInformation("Actualizando usuario: {UserId}", user.Id);
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> UpdateProfilePictureAsync(int userId, string profilePictureUrl)
    {
        var user = await _validationHelper.ValidateUserExistsAsync(userId);

        if (string.IsNullOrWhiteSpace(profilePictureUrl))
            throw new ArgumentException("La URL de la foto de perfil es requerida", nameof(profilePictureUrl));

        user.profile_picture_url = profilePictureUrl;

        _logger.LogInformation("Actualizando foto de perfil del usuario: {UserId}", userId);
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> UpdatePasswordAsync(int userId, string passwordHash)
    {
        var user = await _validationHelper.ValidateUserExistsAsync(userId);

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("El hash de contraseña es requerido", nameof(passwordHash));

        user.password_hash = passwordHash;

        _logger.LogInformation("Actualizando contraseña del usuario: {UserId}", userId);
        await _userRepository.UpdateAsync(user);
        return true;
    }

    // ═══ Eliminar Usuario ═══

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _validationHelper.ValidateUserExistsAsync(userId);

        _logger.LogInformation("Eliminando usuario: {UserId}", userId);
        return await _userRepository.DeleteAsync(userId);
    }

    // ═══ Métodos Especializados ═══

    public async Task<IEnumerable<User>> GetMostActiveUsersAsync(int universityId, int take)
    {
        if (take <= 0)
            return Enumerable.Empty<User>();

        await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo usuarios más activos de universidad {UniversityId}", universityId);
        return await _userRepository.GetMostActiveUsersAsync(universityId, take);
    }

    public async Task<IEnumerable<User>> GetRecentlyRegisteredUsersAsync(int universityId, int take)
    {
        if (take <= 0)
            return Enumerable.Empty<User>();

        await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo usuarios recientemente registrados de universidad {UniversityId}", universityId);
        return await _userRepository.GetRecentlyRegisteredUsersAsync(universityId, take);
    }

    public async Task<IEnumerable<User>> GetSuggestedUsersAsync(int userId, int take)
    {
        var user = await _validationHelper.ValidateUserExistsAsync(userId);

        if (take <= 0)
            return Enumerable.Empty<User>();

        _logger.LogInformation("Obteniendo usuarios sugeridos para: {UserId}", userId);
        return await _userRepository.GetSuggestedUsersAsync(userId, take);
    }

    // ═══ Validaciones ═══

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return false;

        return await _userRepository.UserNameExistsAsync(userName);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return await _userRepository.EmailExistsAsync(email);
    }

    // ═══ Estadísticas ═══

    public async Task<UserStatistics?> GetUserStatisticsAsync(int userId)
    {
        var user = await _validationHelper.ValidateUserExistsAsync(userId);
        _logger.LogInformation("Obteniendo estadísticas del usuario: {UserId}", userId);
        return await _userRepository.GetUserStatisticsAsync(userId);
    }

    public async Task<int> CountUsersByUniversityAsync(int universityId)
    {
        await _validationHelper.ValidateUniversityExistsAsync(universityId);
        return await _userRepository.CountUsersByUniversityAsync(universityId);
    }
}
