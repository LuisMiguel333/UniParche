using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones de likes
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;
    private readonly IMapper _mapper;
    private readonly ILogger<LikesController> _logger;

    public LikesController(ILikeService likeService, IMapper mapper, ILogger<LikesController> logger)
    {
        _likeService = likeService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los likes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<LikeResponse>>>> GetAllLikes()
    {
        try
        {
            var likes = await _likeService.GetAllLikesAsync();
            var likeResponses = _mapper.Map<List<LikeResponse>>(likes);
            return Ok(new ApiResponse<List<LikeResponse>>(likeResponses, "Likes obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener likes");
            return StatusCode(500, new ApiResponse<List<LikeResponse>>("Error al obtener likes", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener un like por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<LikeResponse>>> GetLikeById(int id)
    {
        try
        {
            var like = await _likeService.GetLikeByIdAsync(id);
            if (like == null)
                return NotFound(new ApiResponse<LikeResponse>("Like no encontrado"));

            var likeResponse = _mapper.Map<LikeResponse>(like);
            return Ok(new ApiResponse<LikeResponse>(likeResponse, "Like obtenido exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener like por ID");
            return StatusCode(500, new ApiResponse<LikeResponse>("Error al obtener like", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Crear un nuevo like (dar like a un post)
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<LikeResponse>>> CreateLike([FromBody] CreateLikeRequest request, [FromQuery] int userId)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<LikeResponse>("Datos inválidos"));

            var createdLike = await _likeService.AddLikeAsync(userId, request.PostId);
            var likeResponse = _mapper.Map<LikeResponse>(createdLike);
            return CreatedAtAction(nameof(GetLikeById), new { id = createdLike.Id }, 
                new ApiResponse<LikeResponse>(likeResponse, "Like creado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear like");
            return StatusCode(500, new ApiResponse<LikeResponse>("Error al crear like", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar un like
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteLike(int id)
    {
        try
        {
            var like = await _likeService.GetLikeByIdAsync(id);
            if (like == null)
                return NotFound(new ApiResponse<string>("Like no encontrado"));

            await _likeService.DeleteLikeAsync(id);
            return Ok(new ApiResponse<string>("Like eliminado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar like");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar like", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Verificar si un usuario ha dado like a un post
    /// </summary>
    [HttpGet("user/{userId}/post/{postId}")]
    public async Task<ActionResult<ApiResponse<bool>>> HasUserLikedPost(int userId, int postId)
    {
        try
        {
            var hasLiked = await _likeService.HasUserLikedPostAsync(userId, postId);
            return Ok(new ApiResponse<bool>(hasLiked, "Verificación completada"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar like");
            return StatusCode(500, new ApiResponse<bool>("Error al verificar like", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener likes de un post
    /// </summary>
    [HttpGet("post/{postId}")]
    public async Task<ActionResult<ApiResponse<List<LikeResponse>>>> GetLikesByPostId(int postId)
    {
        try
        {
            var likes = await _likeService.GetLikesByPostIdAsync(postId);
            var likeResponses = _mapper.Map<List<LikeResponse>>(likes);
            return Ok(new ApiResponse<List<LikeResponse>>(likeResponses, "Likes del post obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener likes del post");
            return StatusCode(500, new ApiResponse<List<LikeResponse>>("Error al obtener likes", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener likes de un usuario
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ApiResponse<List<LikeResponse>>>> GetLikesByUserId(int userId)
    {
        try
        {
            var likes = await _likeService.GetLikesByUserIdAsync(userId);
            var likeResponses = _mapper.Map<List<LikeResponse>>(likes);
            return Ok(new ApiResponse<List<LikeResponse>>(likeResponses, "Likes del usuario obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener likes del usuario");
            return StatusCode(500, new ApiResponse<List<LikeResponse>>("Error al obtener likes", new List<string> { ex.Message }));
        }
    }
}
