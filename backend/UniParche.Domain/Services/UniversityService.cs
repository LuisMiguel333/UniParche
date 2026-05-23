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
/// Implementación del servicio de universidades con lógica de negocio
/// </summary>
public class UniversityService : IUniversityService
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IPostRepository _postRepository;
    private readonly UserValidationHelper _validationHelper;
    private readonly ILogger<UniversityService> _logger;

    public UniversityService(
        IUniversityRepository universityRepository,
        IPostRepository postRepository,
        UserValidationHelper validationHelper,
        ILogger<UniversityService> logger)
    {
        _universityRepository = universityRepository ?? throw new ArgumentNullException(nameof(universityRepository));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // ═══ Obtención de Universidades ═══

    public async Task<IEnumerable<University>> GetAllUniversitiesAsync()
    {
        _logger.LogInformation("Obteniendo todas las universidades");
        return await _universityRepository.GetAllAsync();
    }

    public async Task<University?> GetUniversityByIdAsync(int universityId)
    {
        if (universityId <= 0)
            return null;

        _logger.LogInformation("Obteniendo universidad con ID {UniversityId}", universityId);
        return await _universityRepository.GetByIdAsync(universityId);
    }

    public async Task<University?> GetUniversityByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        _logger.LogInformation("Buscando universidad por nombre: {Name}", name);
        return await _universityRepository.GetByNameAsync(name);
    }

    public async Task<University?> GetUniversityByDomainEmailAsync(string domainEmail)
    {
        if (string.IsNullOrWhiteSpace(domainEmail))
            return null;

        _logger.LogInformation("Buscando universidad por dominio");
        return await _universityRepository.GetByDomainEmailAsync(domainEmail);
    }

    public async Task<University?> GetUniversityWithUsersAsync(int universityId)
    {
        var university = await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo universidad con usuarios: {UniversityId}", universityId);
        return await _universityRepository.GetUniversityWithUsersAsync(universityId);
    }

    public async Task<University?> GetUniversityWithPostsAsync(int universityId)
    {
        var university = await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo universidad con posts: {UniversityId}", universityId);
        return await _universityRepository.GetUniversityWithPostsAsync(universityId);
    }

    public async Task<IEnumerable<University>> SearchUniversitiesAsync(string searchTerm)
    {
        UserValidationHelper.ValidateSearchTerm(searchTerm);
        _logger.LogInformation("Buscando universidades: {SearchTerm}", searchTerm);
        return await _universityRepository.SearchUniversitiesAsync(searchTerm);
    }

    public async Task<IEnumerable<University>> GetUniversitiesWithUserCountAsync()
    {
        _logger.LogInformation("Obteniendo universidades con conteos de usuarios");
        return await _universityRepository.GetUniversitiesWithUserCountAsync();
    }

    // ═══ Crear, Actualizar y Eliminar Universidades ═══

    public async Task<University> CreateUniversityAsync(University university)
    {
        if (university == null)
            throw new ArgumentNullException(nameof(university));

        // Validaciones
        await _validationHelper.ValidateUniversityNameAsync(university.Name);
        await _validationHelper.ValidateUniversityDomainAsync(university.DomainEmail);

        _logger.LogInformation("Creando nueva universidad: {Name}", university.Name);
        return await _universityRepository.AddAsync(university);
    }

    public async Task<University> UpdateUniversityAsync(University university)
    {
        if (university == null)
            throw new ArgumentNullException(nameof(university));

        if (university.Id <= 0)
            throw new ArgumentException("La universidad debe tener un ID válido", nameof(university.Id));

        var existingUniversity = await _validationHelper.ValidateUniversityExistsAsync(university.Id);

        _logger.LogInformation("Actualizando universidad: {UniversityId}", university.Id);
        return await _universityRepository.UpdateAsync(university);
    }

    public async Task<bool> DeleteUniversityAsync(int universityId)
    {
        var university = await _validationHelper.ValidateUniversityExistsAsync(universityId);

        _logger.LogInformation("Eliminando universidad: {UniversityId}", universityId);
        return await _universityRepository.DeleteAsync(universityId);
    }

    // ═══ Universidades Destacadas ═══

    public async Task<IEnumerable<University>> GetMostActiveUniversitiesAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<University>();

        _logger.LogInformation("Obteniendo {Count} universidades más activas", take);
        return await _universityRepository.GetMostActiveUniversitiesAsync(take);
    }

    public async Task<IEnumerable<University>> GetUniversitiesWithMostUsersAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<University>();

        _logger.LogInformation("Obteniendo universidades con más usuarios");
        return await _universityRepository.GetUniversitiesWithMostUsersAsync(take);
    }

    public async Task<IEnumerable<University>> GetTrendingUniversitiesAsync(int take)
    {
        if (take <= 0)
            return Enumerable.Empty<University>();

        _logger.LogInformation("Obteniendo universidades en tendencia");
        return await _universityRepository.GetMostActiveUniversitiesAsync(take);
    }

    // ═══ Feed y Consultas Especializadas ═══

    public async Task<(IEnumerable<Post> posts, int totalCount)> GetUniversityFeedAsync(int universityId, int pageNumber, int pageSize)
    {
        UserValidationHelper.ValidatePagination(pageNumber, pageSize);
        var university = await _validationHelper.ValidateUniversityExistsAsync(universityId);

        _logger.LogInformation("Obteniendo feed de universidad {UniversityId} - Página {Page}", universityId, pageNumber);

        var posts = await _postRepository.GetUniversityFeedAsync(universityId, (pageNumber - 1) * pageSize, pageSize);
        var totalCount = await _postRepository.CountPostsByUniversityAsync(universityId);

        return (posts, totalCount);
    }

    // ═══ Validaciones ═══

    public async Task<bool> UniversityExistsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        return await _universityRepository.UniversityExistsByNameAsync(name);
    }

    public async Task<bool> UniversityExistsByDomainAsync(string domainEmail)
    {
        if (string.IsNullOrWhiteSpace(domainEmail))
            return false;

        return await _universityRepository.UniversityExistsByDomainAsync(domainEmail);
    }

    // ═══ Estadísticas ═══

    public async Task<UniversityStatistics?> GetUniversityStatisticsAsync(int universityId)
    {
        var university = await _validationHelper.ValidateUniversityExistsAsync(universityId);
        _logger.LogInformation("Obteniendo estadísticas de universidad: {UniversityId}", universityId);
        return await _universityRepository.GetUniversityStatisticsAsync(universityId);
    }

    public async Task<int> CountUniversitiesAsync()
    {
        return await _universityRepository.CountAsync();
    }

    public async Task<(IEnumerable<University> universities, int totalCount)> GetUniversitiesPaginatedAsync(int pageNumber, int pageSize)
    {
        UserValidationHelper.ValidatePagination(pageNumber, pageSize);
        _logger.LogInformation("Obteniendo universidades paginadas - Página {Page}", pageNumber);

        var universities = await _universityRepository.GetAllAsync();
        var totalCount = universities.Count();

        var paginatedUniversities = universities
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paginatedUniversities, totalCount);
    }
}
