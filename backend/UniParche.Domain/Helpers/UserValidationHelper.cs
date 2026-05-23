using System;
using System.Collections.Generic;
using System.Text;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Repositories;

namespace UniParche.Domain.Helpers;

/// <summary>
/// Helper para validaciones comunes de usuarios
/// </summary>
public class UserValidationHelper
{
    private readonly IUserRepository _userRepository;
    private readonly IUniversityRepository _universityRepository;

    public UserValidationHelper(IUserRepository userRepository, IUniversityRepository universityRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _universityRepository = universityRepository ?? throw new ArgumentNullException(nameof(universityRepository));
    }

    /// <summary>
    /// Valida que un usuario exista
    /// </summary>
    public async Task<User> ValidateUserExistsAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"No se encontró el usuario con ID {userId}");
        return user;
    }

    /// <summary>
    /// Valida que una universidad exista
    /// </summary>
    public async Task<University> ValidateUniversityExistsAsync(int universityId)
    {
        var university = await _universityRepository.GetByIdAsync(universityId);
        if (university == null)
            throw new KeyNotFoundException($"No se encontró la universidad con ID {universityId}");
        return university;
    }

    /// <summary>
    /// Valida que un email sea válido y no esté duplicado
    /// </summary>
    public async Task ValidateEmailAsync(string email, int? excludeUserId = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email es requerido", nameof(email));

        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null && (excludeUserId == null || existingUser.Id != excludeUserId))
            throw new InvalidOperationException($"Ya existe un usuario con el email {email}");
    }

    /// <summary>
    /// Valida que un nombre de usuario no sea duplicado
    /// </summary>
    public async Task ValidateUserNameAsync(string userName, int? excludeUserId = null)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return;

        var userNameExists = await _userRepository.UserNameExistsAsync(userName);
        if (userNameExists)
            throw new InvalidOperationException($"El nombre de usuario {userName} ya está en uso");
    }

    /// <summary>
    /// Valida que un dominio de email de universidad no sea duplicado
    /// </summary>
    public async Task ValidateUniversityDomainAsync(string domain, int? excludeUniversityId = null)
    {
        if (string.IsNullOrWhiteSpace(domain))
            throw new ArgumentException("El dominio es requerido", nameof(domain));

        var domainExists = await _universityRepository.UniversityExistsByDomainAsync(domain);
        if (domainExists)
            throw new InvalidOperationException($"Ya existe una universidad con el dominio {domain}");
    }

    /// <summary>
    /// Valida que un nombre de universidad no sea duplicado
    /// </summary>
    public async Task ValidateUniversityNameAsync(string name, int? excludeUniversityId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre es requerido", nameof(name));

        var nameExists = await _universityRepository.UniversityExistsByNameAsync(name);
        if (nameExists)
            throw new InvalidOperationException($"Ya existe una universidad con el nombre {name}");
    }

    /// <summary>
    /// Valida valores de paginación
    /// </summary>
    public static void ValidatePagination(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0)
            throw new ArgumentException("El número de página debe ser mayor a 0", nameof(pageNumber));
        if (pageSize <= 0)
            throw new ArgumentException("El tamaño de página debe ser mayor a 0", nameof(pageSize));
        if (pageSize > 100)
            throw new ArgumentException("El tamaño de página no puede ser mayor a 100", nameof(pageSize));
    }

    /// <summary>
    /// Valida parámetros de búsqueda
    /// </summary>
    public static void ValidateSearchTerm(string searchTerm, string paramName = "searchTerm")
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            throw new ArgumentException("El término de búsqueda no puede estar vacío", paramName);
        if (searchTerm.Length < 2)
            throw new ArgumentException("El término de búsqueda debe tener al menos 2 caracteres", paramName);
    }
}
