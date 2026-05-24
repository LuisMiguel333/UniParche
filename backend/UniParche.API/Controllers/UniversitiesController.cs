using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones de universidades
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UniversitiesController : ControllerBase
{
    private readonly IUniversityService _universityService;
    private readonly IMapper _mapper;
    private readonly ILogger<UniversitiesController> _logger;

    public UniversitiesController(IUniversityService universityService, IMapper mapper, ILogger<UniversitiesController> logger)
    {
        _universityService = universityService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todas las universidades
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<UniversityResponse>>>> GetAllUniversities()
    {
        try
        {
            var universities = await _universityService.GetAllUniversitiesAsync();
            var universityResponses = _mapper.Map<List<UniversityResponse>>(universities);
            return Ok(new ApiResponse<List<UniversityResponse>>(universityResponses, "Universidades obtenidas exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener universidades");
            return StatusCode(500, new ApiResponse<List<UniversityResponse>>("Error al obtener universidades", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener una universidad por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UniversityResponse>>> GetUniversityById(int id)
    {
        try
        {
            var university = await _universityService.GetUniversityByIdAsync(id);
            if (university == null)
                return NotFound(new ApiResponse<UniversityResponse>("Universidad no encontrada"));

            var universityResponse = _mapper.Map<UniversityResponse>(university);
            return Ok(new ApiResponse<UniversityResponse>(universityResponse, "Universidad obtenida exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener universidad por ID");
            return StatusCode(500, new ApiResponse<UniversityResponse>("Error al obtener universidad", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Crear una nueva universidad
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<UniversityResponse>>> CreateUniversity([FromBody] CreateUniversityRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<UniversityResponse>("Datos inválidos"));

            var university = _mapper.Map<University>(request);
            var createdUniversity = await _universityService.CreateUniversityAsync(university);
            var universityResponse = _mapper.Map<UniversityResponse>(createdUniversity);
            return CreatedAtAction(nameof(GetUniversityById), new { id = createdUniversity.Id }, 
                new ApiResponse<UniversityResponse>(universityResponse, "Universidad creada exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear universidad");
            return StatusCode(500, new ApiResponse<UniversityResponse>("Error al crear universidad", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar una universidad
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UniversityResponse>>> UpdateUniversity(int id, [FromBody] UpdateUniversityRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<UniversityResponse>("Datos inválidos"));

            var university = await _universityService.GetUniversityByIdAsync(id);
            if (university == null)
                return NotFound(new ApiResponse<UniversityResponse>("Universidad no encontrada"));

            _mapper.Map(request, university);
            var updatedUniversity = await _universityService.UpdateUniversityAsync(university);
            var universityResponse = _mapper.Map<UniversityResponse>(updatedUniversity);
            return Ok(new ApiResponse<UniversityResponse>(universityResponse, "Universidad actualizada exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar universidad");
            return StatusCode(500, new ApiResponse<UniversityResponse>("Error al actualizar universidad", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar una universidad
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteUniversity(int id)
    {
        try
        {
            var university = await _universityService.GetUniversityByIdAsync(id);
            if (university == null)
                return NotFound(new ApiResponse<string>("Universidad no encontrada"));

            await _universityService.DeleteUniversityAsync(id);
            return Ok(new ApiResponse<string>("Universidad eliminada exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar universidad");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar universidad", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener estadísticas de una universidad
    /// </summary>
    [HttpGet("{id}/statistics")]
    public async Task<ActionResult<ApiResponse<UniversityStatisticsResponse>>> GetUniversityStatistics(int id)
    {
        try
        {
            var statistics = await _universityService.GetUniversityStatisticsAsync(id);
            if (statistics == null)
                return NotFound(new ApiResponse<UniversityStatisticsResponse>("Estadísticas no encontradas"));

            var statsResponse = new UniversityStatisticsResponse
            {
                UniversityId = statistics.UniversityId,
                UniversityName = statistics.UniversityName,
                TotalUsers = statistics.TotalUsers,
                TotalPosts = statistics.TotalPosts,
                TotalComments = statistics.TotalComments,
                TotalLikes = statistics.TotalLikes,
                CreatedAt = DateTime.Now // Placeholder
            };

            return Ok(new ApiResponse<UniversityStatisticsResponse>(statsResponse, "Estadísticas obtenidas exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estadísticas de la universidad");
            return StatusCode(500, new ApiResponse<UniversityStatisticsResponse>("Error al obtener estadísticas", new List<string> { ex.Message }));
        }
    }
}
