using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones de comentarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(ICommentService commentService, IMapper mapper, ILogger<CommentsController> logger)
    {
        _commentService = commentService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los comentarios
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CommentResponse>>>> GetAllComments()
    {
        try
        {
            var comments = await _commentService.GetAllCommentsAsync();
            var commentResponses = _mapper.Map<List<CommentResponse>>(comments);
            return Ok(new ApiResponse<List<CommentResponse>>(commentResponses, "Comentarios obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener comentarios");
            return StatusCode(500, new ApiResponse<List<CommentResponse>>("Error al obtener comentarios", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener un comentario por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<CommentResponse>>> GetCommentById(int id)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound(new ApiResponse<CommentResponse>("Comentario no encontrado"));

            var commentResponse = _mapper.Map<CommentResponse>(comment);
            return Ok(new ApiResponse<CommentResponse>(commentResponse, "Comentario obtenido exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener comentario por ID");
            return StatusCode(500, new ApiResponse<CommentResponse>("Error al obtener comentario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Crear un nuevo comentario
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CommentResponse>>> CreateComment([FromBody] CreateCommentRequest request, [FromQuery] int userId)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<CommentResponse>("Datos inválidos"));

            var comment = _mapper.Map<Comment>(request);
            comment.UserId = userId;
            var createdComment = await _commentService.CreateCommentAsync(comment);
            var commentResponse = _mapper.Map<CommentResponse>(createdComment);
            return CreatedAtAction(nameof(GetCommentById), new { id = createdComment.Id }, 
                new ApiResponse<CommentResponse>(commentResponse, "Comentario creado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear comentario");
            return StatusCode(500, new ApiResponse<CommentResponse>("Error al crear comentario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar un comentario
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<CommentResponse>>> UpdateComment(int id, [FromBody] UpdateCommentRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<CommentResponse>("Datos inválidos"));

            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound(new ApiResponse<CommentResponse>("Comentario no encontrado"));

            _mapper.Map(request, comment);
            var updatedComment = await _commentService.UpdateCommentAsync(comment);
            var commentResponse = _mapper.Map<CommentResponse>(updatedComment);
            return Ok(new ApiResponse<CommentResponse>(commentResponse, "Comentario actualizado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar comentario");
            return StatusCode(500, new ApiResponse<CommentResponse>("Error al actualizar comentario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar un comentario
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteComment(int id)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound(new ApiResponse<string>("Comentario no encontrado"));

            await _commentService.DeleteCommentAsync(id);
            return Ok(new ApiResponse<string>("Comentario eliminado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar comentario");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar comentario", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener comentarios de un post
    /// </summary>
    [HttpGet("post/{postId}")]
    public async Task<ActionResult<ApiResponse<List<CommentResponse>>>> GetCommentsByPostId(int postId)
    {
        try
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            var commentResponses = _mapper.Map<List<CommentResponse>>(comments);
            return Ok(new ApiResponse<List<CommentResponse>>(commentResponses, "Comentarios del post obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener comentarios del post");
            return StatusCode(500, new ApiResponse<List<CommentResponse>>("Error al obtener comentarios", new List<string> { ex.Message }));
        }
    }
}
