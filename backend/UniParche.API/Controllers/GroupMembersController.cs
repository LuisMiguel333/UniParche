using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar los miembros de un grupo
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GroupMembersController : ControllerBase
{
    private readonly IGroupMemberService _memberService;
    private readonly IMapper _mapper;
    private readonly ILogger<GroupMembersController> _logger;

    public GroupMembersController(IGroupMemberService memberService, IMapper mapper, ILogger<GroupMembersController> logger)
    {
        _memberService = memberService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los miembros de un grupo
    /// </summary>
    [HttpGet("group/{groupId}")]
    public async Task<ActionResult<ApiResponse<List<GroupMemberResponse>>>> GetByGroup(int groupId)
    {
        try
        {
            var members = await _memberService.GetByGroupAsync(groupId);
            var response = _mapper.Map<List<GroupMemberResponse>>(members);
            return Ok(new ApiResponse<List<GroupMemberResponse>>(response, "Miembros obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener miembros del grupo");
            return StatusCode(500, new ApiResponse<List<GroupMemberResponse>>("Error al obtener miembros", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener todos los grupos de un usuario
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ApiResponse<List<GroupMemberResponse>>>> GetByUser(int userId)
    {
        try
        {
            var members = await _memberService.GetByUserAsync(userId);
            var response = _mapper.Map<List<GroupMemberResponse>>(members);
            return Ok(new ApiResponse<List<GroupMemberResponse>>(response, "Grupos del usuario obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener grupos del usuario");
            return StatusCode(500, new ApiResponse<List<GroupMemberResponse>>("Error al obtener grupos", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Unirse a un grupo
    /// </summary>
    [HttpPost("group/{groupId}/user/{userId}")]
    public async Task<ActionResult<ApiResponse<GroupMemberResponse>>> Join(int groupId, int userId)
    {
        try
        {
            var member = await _memberService.JoinGroupAsync(groupId, userId);
            var response = _mapper.Map<GroupMemberResponse>(member);
            return Ok(new ApiResponse<GroupMemberResponse>(response, "Te has unido al grupo exitosamente"));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<GroupMemberResponse>(ex.Message));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<GroupMemberResponse>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al unirse al grupo");
            return StatusCode(500, new ApiResponse<GroupMemberResponse>("Error al unirse al grupo", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar el rol de un miembro en el grupo
    /// </summary>
    [HttpPatch("group/{groupId}/user/{userId}/role")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateRole(int groupId, int userId, [FromQuery] string role)
    {
        try
        {
            await _memberService.UpdateRoleAsync(groupId, userId, role);
            return Ok(new ApiResponse<string>("Rol actualizado exitosamente"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar rol del miembro");
            return StatusCode(500, new ApiResponse<string>("Error al actualizar rol", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Salir de un grupo
    /// </summary>
    [HttpDelete("group/{groupId}/user/{userId}")]
    public async Task<ActionResult<ApiResponse<string>>> Leave(int groupId, int userId)
    {
        try
        {
            await _memberService.LeaveGroupAsync(groupId, userId);
            return Ok(new ApiResponse<string>("Has salido del grupo exitosamente"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al salir del grupo");
            return StatusCode(500, new ApiResponse<string>("Error al salir del grupo", new List<string> { ex.Message }));
        }
    }
}