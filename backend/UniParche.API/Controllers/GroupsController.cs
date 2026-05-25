using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar los grupos universitarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly IMapper _mapper;
    private readonly ILogger<GroupsController> _logger;

    public GroupsController(IGroupService groupService, IMapper mapper, ILogger<GroupsController> logger)
    {
        _groupService = groupService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los grupos
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GroupResponse>>>> GetAll()
    {
        try
        {
            var groups = await _groupService.GetAllAsync();
            var response = _mapper.Map<List<GroupResponse>>(groups);
            return Ok(new ApiResponse<List<GroupResponse>>(response, "Grupos obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener grupos");
            return StatusCode(500, new ApiResponse<List<GroupResponse>>("Error al obtener grupos", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener un grupo por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GroupResponse>>> GetById(int id)
    {
        try
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return NotFound(new ApiResponse<GroupResponse>("Grupo no encontrado"));

            var response = _mapper.Map<GroupResponse>(group);
            return Ok(new ApiResponse<GroupResponse>(response, "Grupo obtenido exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener grupo por ID");
            return StatusCode(500, new ApiResponse<GroupResponse>("Error al obtener grupo", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener grupos por universidad
    /// </summary>
    [HttpGet("university/{universityId}")]
    public async Task<ActionResult<ApiResponse<List<GroupResponse>>>> GetByUniversity(int universityId)
    {
        try
        {
            var groups = await _groupService.GetByUniversityAsync(universityId);
            var response = _mapper.Map<List<GroupResponse>>(groups);
            return Ok(new ApiResponse<List<GroupResponse>>(response, "Grupos de la universidad obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener grupos por universidad");
            return StatusCode(500, new ApiResponse<List<GroupResponse>>("Error al obtener grupos", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Crear un nuevo grupo
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<GroupResponse>>> Create([FromBody] CreateGroupRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<GroupResponse>("Datos inválidos"));

            var created = await _groupService.CreateAsync(request);
            var response = _mapper.Map<GroupResponse>(created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                new ApiResponse<GroupResponse>(response, "Grupo creado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear grupo");
            return StatusCode(500, new ApiResponse<GroupResponse>("Error al crear grupo", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar un grupo existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<GroupResponse>>> Update(int id, [FromBody] UpdateGroupRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<GroupResponse>("Datos inválidos"));

            var updated = await _groupService.UpdateAsync(id, request);
            if (updated == null)
                return NotFound(new ApiResponse<GroupResponse>("Grupo no encontrado"));

            var response = _mapper.Map<GroupResponse>(updated);
            return Ok(new ApiResponse<GroupResponse>(response, "Grupo actualizado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar grupo");
            return StatusCode(500, new ApiResponse<GroupResponse>("Error al actualizar grupo", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar un grupo por ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        try
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return NotFound(new ApiResponse<string>("Grupo no encontrado"));

            await _groupService.DeleteAsync(id);
            return Ok(new ApiResponse<string>("Grupo eliminado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar grupo");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar grupo", new List<string> { ex.Message }));
        }
    }
}