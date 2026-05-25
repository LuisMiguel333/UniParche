using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar los asistentes de un parche
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EventAttendeesController : ControllerBase
{
    private readonly IEventAttendeeService _attendeeService;
    private readonly IMapper _mapper;
    private readonly ILogger<EventAttendeesController> _logger;

    public EventAttendeesController(IEventAttendeeService attendeeService, IMapper mapper, ILogger<EventAttendeesController> logger)
    {
        _attendeeService = attendeeService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los asistentes de un parche
    /// </summary>
    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<ApiResponse<List<EventAttendeeResponse>>>> GetByEvent(int eventId)
    {
        try
        {
            var attendees = await _attendeeService.GetByEventAsync(eventId);
            var response = _mapper.Map<List<EventAttendeeResponse>>(attendees);
            return Ok(new ApiResponse<List<EventAttendeeResponse>>(response, "Asistentes obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener asistentes del parche");
            return StatusCode(500, new ApiResponse<List<EventAttendeeResponse>>("Error al obtener asistentes", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener todos los parches a los que asiste un usuario
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ApiResponse<List<EventAttendeeResponse>>>> GetByUser(int userId)
    {
        try
        {
            var attendees = await _attendeeService.GetByUserAsync(userId);
            var response = _mapper.Map<List<EventAttendeeResponse>>(attendees);
            return Ok(new ApiResponse<List<EventAttendeeResponse>>(response, "Inscripciones del usuario obtenidas exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener inscripciones del usuario");
            return StatusCode(500, new ApiResponse<List<EventAttendeeResponse>>("Error al obtener inscripciones", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Inscribir un usuario en un parche
    /// </summary>
    [HttpPost("event/{eventId}/user/{userId}")]
    public async Task<ActionResult<ApiResponse<EventAttendeeResponse>>> Join(int eventId, int userId)
    {
        try
        {
            var attendee = await _attendeeService.JoinEventAsync(eventId, userId);
            var response = _mapper.Map<EventAttendeeResponse>(attendee);
            return Ok(new ApiResponse<EventAttendeeResponse>(response, "Inscripción realizada exitosamente"));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<EventAttendeeResponse>(ex.Message));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<EventAttendeeResponse>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al inscribir usuario en parche");
            return StatusCode(500, new ApiResponse<EventAttendeeResponse>("Error al inscribir usuario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar el estado de asistencia de un usuario en un parche
    /// </summary>
    [HttpPatch("event/{eventId}/user/{userId}/status")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateStatus(int eventId, int userId, [FromQuery] string status)
    {
        try
        {
            await _attendeeService.UpdateStatusAsync(eventId, userId, status);
            return Ok(new ApiResponse<string>("Estado actualizado exitosamente"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar estado de asistencia");
            return StatusCode(500, new ApiResponse<string>("Error al actualizar estado", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Retirar la inscripción de un usuario en un parche
    /// </summary>
    [HttpDelete("event/{eventId}/user/{userId}")]
    public async Task<ActionResult<ApiResponse<string>>> Leave(int eventId, int userId)
    {
        try
        {
            await _attendeeService.LeaveEventAsync(eventId, userId);
            return Ok(new ApiResponse<string>("Inscripción eliminada exitosamente"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al retirar inscripción");
            return StatusCode(500, new ApiResponse<string>("Error al retirar inscripción", new List<string> { ex.Message }));
        }
    }
}