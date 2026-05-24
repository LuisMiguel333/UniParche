using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones de usuarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los usuarios
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<UserResponse>>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return Ok(new ApiResponse<List<UserResponse>>(userResponses, "Usuarios obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuarios");
            return StatusCode(500, new ApiResponse<List<UserResponse>>("Error al obtener usuarios", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener un usuario por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse<UserResponse>("Usuario no encontrado"));
            }
            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(new ApiResponse<UserResponse>(userResponse, "Usuario obtenido exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuario por ID");
            return StatusCode(500, new ApiResponse<UserResponse>("Error al obtener usuario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Crear un nuevo usuario
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserResponse>>> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<UserResponse>("Datos inválidos"));

            var user = _mapper.Map<User>(request);
            var createdUser = await _userService.CreateUserAsync(user);
            var userResponse = _mapper.Map<UserResponse>(createdUser);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, 
                new ApiResponse<UserResponse>(userResponse, "Usuario creado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear usuario");
            return StatusCode(500, new ApiResponse<UserResponse>("Error al crear usuario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar un usuario
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<UserResponse>("Datos inválidos"));

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<UserResponse>("Usuario no encontrado"));

            _mapper.Map(request, user);
            var updatedUser = await _userService.UpdateUserAsync(user);
            var userResponse = _mapper.Map<UserResponse>(updatedUser);
            return Ok(new ApiResponse<UserResponse>(userResponse, "Usuario actualizado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar usuario");
            return StatusCode(500, new ApiResponse<UserResponse>("Error al actualizar usuario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Cambiar contraseña de usuario
    /// </summary>
    [HttpPut("{id}/change-password")]
    public async Task<ActionResult<ApiResponse<string>>> ChangePassword(int id, [FromBody] UpdatePasswordRequest request)
    {
        try
        {
            if (!ModelState.IsValid || request.NewPassword != request.ConfirmPassword)
                return BadRequest(new ApiResponse<string>("Las contraseñas no coinciden o datos inválidos"));

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<string>("Usuario no encontrado"));

            // Hash de la nueva contraseña
            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.NewPassword);
            await _userService.UpdatePasswordAsync(id, passwordHash);
            return Ok(new ApiResponse<string>("Contraseña cambiada exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar contraseña");
            return StatusCode(500, new ApiResponse<string>("Error al cambiar contraseña", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar un usuario
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteUser(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<string>("Usuario no encontrado"));

            await _userService.DeleteUserAsync(id);
            return Ok(new ApiResponse<string>("Usuario eliminado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar usuario");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar usuario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener usuarios por universidad
    /// </summary>
    [HttpGet("university/{universityId}")]
    public async Task<ActionResult<ApiResponse<List<UserResponse>>>> GetUsersByUniversity(int universityId)
    {
        try
        {
            var users = await _userService.GetUsersByUniversityAsync(universityId);
            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return Ok(new ApiResponse<List<UserResponse>>(userResponses, "Usuarios obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuarios de la universidad");
            return StatusCode(500, new ApiResponse<List<UserResponse>>("Error al obtener usuarios", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener estadísticas de un usuario
    /// </summary>
    [HttpGet("{id}/statistics")]
    public async Task<ActionResult<ApiResponse<UserStatisticsResponse>>> GetUserStatistics(int id)
    {
        try
        {
            var statistics = await _userService.GetUserStatisticsAsync(id);
            if (statistics == null)
                return NotFound(new ApiResponse<UserStatisticsResponse>("Estadísticas no encontradas"));

            var statsResponse = new UserStatisticsResponse
            {
                UserId = statistics.UserId,
                UserName = statistics.UserName,
                TotalPosts = statistics.TotalPosts,
                TotalComments = statistics.TotalComments,
                TotalLikesGiven = statistics.TotalLikesGiven,
                TotalLikesReceived = statistics.TotalLikesReceived,
                RegisterDate = statistics.RegisterDate
            };

            return Ok(new ApiResponse<UserStatisticsResponse>(statsResponse, "Estadísticas obtenidas exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estadísticas del usuario");
            return StatusCode(500, new ApiResponse<UserStatisticsResponse>("Error al obtener estadísticas", new List<string> { ex.Message }));
        }
    }
}
