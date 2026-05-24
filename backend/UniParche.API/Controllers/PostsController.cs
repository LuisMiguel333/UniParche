using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniParche.Domain.Entities;
using UniParche.Domain.Interfaces.Services;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Controllers;

/// <summary>
/// Controlador para gestionar operaciones de posts
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    private readonly ILogger<PostsController> _logger;

    public PostsController(IPostService postService, IMapper mapper, ILogger<PostsController> logger)
    {
        _postService = postService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtener todos los posts
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<PostResponse>>>> GetAllPosts()
    {
        try
        {
            var posts = await _postService.GetAllPostsAsync();
            var postResponses = _mapper.Map<List<PostResponse>>(posts);
            return Ok(new ApiResponse<List<PostResponse>>(postResponses, "Posts obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener posts");
            return StatusCode(500, new ApiResponse<List<PostResponse>>("Error al obtener posts", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener un post por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PostResponse>>> GetPostById(int id)
    {
        try
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound(new ApiResponse<PostResponse>("Post no encontrado"));

            var postResponse = _mapper.Map<PostResponse>(post);
            return Ok(new ApiResponse<PostResponse>(postResponse, "Post obtenido exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener post por ID");
            return StatusCode(500, new ApiResponse<PostResponse>("Error al obtener post", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Crear un nuevo post
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<PostResponse>>> CreatePost([FromBody] CreatePostRequest request, [FromQuery] int userId)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<PostResponse>("Datos inválidos"));

            var post = _mapper.Map<Post>(request);
            post.UserId = userId;
            var createdPost = await _postService.CreatePostAsync(post);
            var postResponse = _mapper.Map<PostResponse>(createdPost);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, 
                new ApiResponse<PostResponse>(postResponse, "Post creado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear post");
            return StatusCode(500, new ApiResponse<PostResponse>("Error al crear post", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Actualizar un post
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<PostResponse>>> UpdatePost(int id, [FromBody] UpdatePostRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<PostResponse>("Datos inválidos"));

            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound(new ApiResponse<PostResponse>("Post no encontrado"));

            _mapper.Map(request, post);
            var updatedPost = await _postService.UpdatePostAsync(post);
            var postResponse = _mapper.Map<PostResponse>(updatedPost);
            return Ok(new ApiResponse<PostResponse>(postResponse, "Post actualizado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar post");
            return StatusCode(500, new ApiResponse<PostResponse>("Error al actualizar post", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Eliminar un post
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> DeletePost(int id)
    {
        try
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound(new ApiResponse<string>("Post no encontrado"));

            await _postService.DeletePostAsync(id);
            return Ok(new ApiResponse<string>("Post eliminado exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar post");
            return StatusCode(500, new ApiResponse<string>("Error al eliminar post", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener posts de un usuario
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ApiResponse<List<PostResponse>>>> GetPostsByUserId(int userId)
    {
        try
        {
            var posts = await _postService.GetPostsByUserIdAsync(userId);
            var postResponses = _mapper.Map<List<PostResponse>>(posts);
            return Ok(new ApiResponse<List<PostResponse>>(postResponses, "Posts del usuario obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener posts del usuario");
            return StatusCode(500, new ApiResponse<List<PostResponse>>("Error al obtener posts", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Obtener posts recientes
    /// </summary>
    [HttpGet("recent")]
    public async Task<ActionResult<ApiResponse<List<PostResponse>>>> GetRecentPosts([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        try
        {
            var posts = await _postService.GetRecentPostsAsync(skip, take);
            var postResponses = _mapper.Map<List<PostResponse>>(posts);
            return Ok(new ApiResponse<List<PostResponse>>(postResponses, "Posts recientes obtenidos exitosamente"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener posts recientes");
            return StatusCode(500, new ApiResponse<List<PostResponse>>("Error al obtener posts recientes", new List<string> { ex.Message }));
        }
    }
}
