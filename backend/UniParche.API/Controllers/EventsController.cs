using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Interfaces.Services;
using UniParche.Domain.Entities;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar los parches (eventos universitarios)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IEventService eventService, IMapper mapper, ILogger<EventsController> logger)
    {
        _eventService = eventService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los parches
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetAll()
    {
        try
        {
            var events = await _eventService.GetAllAsync();
            var response = _mapper.Map<List<EventResponse>>(events);
            return Ok(new ApiResponse<List<EventResponse>>(response, "Parches obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener parches");
            return StatusCode(500, new ApiResponse<List<EventResponse>>("Error al obtener parches", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener un parche por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<EventResponse>>> GetById(int id)
    {
        try
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null)
                return NotFound(new ApiResponse<EventResponse>("Parche no encontrado"));

            var response = _mapper.Map<EventResponse>(ev);
            return Ok(new ApiResponse<EventResponse>(response, "Parche obtenido exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener parche por ID");
            return StatusCode(500, new ApiResponse<EventResponse>("Error al obtener parche", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener parches por universidad
    /// </summary>
    [HttpGet("university/{universityId}")]
    public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetByUniversity(int universityId)
    {
        try
        {
            var events = await _eventService.GetByUniversityAsync(universityId);
            var response = _mapper.Map<List<EventResponse>>(events);
            return Ok(new ApiResponse<List<EventResponse>>(response, "Parches de la universidad obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener parches por universidad");
            return StatusCode(500, new ApiResponse<List<EventResponse>>("Error al obtener parches", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Crear un nuevo parche
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<EventResponse>>> Create([FromBody] CreateEventRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<EventResponse>("Datos inválidos"));

            var entity = _mapper.Map<Event>(request);
            var created = await _eventService.CreateAsync(entity);
            var response = _mapper.Map<EventResponse>(created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                new ApiResponse<EventResponse>(response, "Parche creado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear parche");
            return StatusCode(500, new ApiResponse<EventResponse>("Error al crear parche", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar un parche existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<EventResponse>>> Update(int id, [FromBody] UpdateEventRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<EventResponse>("Datos inválidos"));

            var entity = _mapper.Map<Event>(request);
            var updated = await _eventService.UpdateAsync(id, entity);
            if (updated == null)
                return NotFound(new ApiResponse<EventResponse>("Parche no encontrado"));

            var response = _mapper.Map<EventResponse>(updated);
            return Ok(new ApiResponse<EventResponse>(response, "Parche actualizado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar parche");
            return StatusCode(500, new ApiResponse<EventResponse>("Error al actualizar parche", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar un parche por ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        try
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null)
                return NotFound(new ApiResponse<string>("Parche no encontrado"));

            await _eventService.DeleteAsync(id);
            return Ok(new ApiResponse<string>("Parche eliminado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar parche");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar parche", new List<string> { ex.Message }));
        }
    }
}