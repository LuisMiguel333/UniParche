using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar las amistades entre usuarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FriendshipsController : ControllerBase
{
    private readonly IFriendshipService _friendshipService;
    private readonly IMapper _mapper;
    private readonly ILogger<FriendshipsController> _logger;

    public FriendshipsController(IFriendshipService friendshipService, IMapper mapper, ILogger<FriendshipsController> logger)
    {
        _friendshipService = friendshipService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todas las amistades de un usuario
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ApiResponse<List<FriendshipResponse>>>> GetByUser(int userId)
    {
        try
        {
            var friendships = await _friendshipService.GetByUserAsync(userId);
            var response = _mapper.Map<List<FriendshipResponse>>(friendships);
            return Ok(new ApiResponse<List<FriendshipResponse>>(response, "Amistades obtenidas exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener amistades del usuario");
            return StatusCode(500, new ApiResponse<List<FriendshipResponse>>("Error al obtener amistades", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Enviar solicitud de amistad
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<FriendshipResponse>>> SendRequest([FromBody] CreateFriendshipRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<FriendshipResponse>("Datos inválidos"));

            var friendship = await _friendshipService.SendRequestAsync(request);
            var response = _mapper.Map<FriendshipResponse>(friendship);
            return Ok(new ApiResponse<FriendshipResponse>(response, "Solicitud de amistad enviada exitosamente"));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<FriendshipResponse>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al enviar solicitud de amistad");
            return StatusCode(500, new ApiResponse<FriendshipResponse>("Error al enviar solicitud", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar el estado de una amistad (aceptar/rechazar)
    /// </summary>
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateStatus(int id, [FromBody] UpdateFriendshipRequest request)
    {
        try
        {
            await _friendshipService.UpdateStatusAsync(id, request);
            return Ok(new ApiResponse<string>("Estado de amistad actualizado exitosamente"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar estado de amistad");
            return StatusCode(500, new ApiResponse<string>("Error al actualizar estado", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar una amistad
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        try
        {
            await _friendshipService.DeleteAsync(id);
            return Ok(new ApiResponse<string>("Amistad eliminada exitosamente"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar amistad");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar amistad", new List<string> { ex.Message }));
        }
    }
}