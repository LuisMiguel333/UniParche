using Microsoft.EntityFrameworkCore;
using UniParche.DataAccess.DbContext;
using UniParche.DataAccess.Repositories;
using UniParche.Domain.Interfaces.Repositories;
using UniParche.Domain.Interfaces.Services;
using UniParche.Domain.Services;
using UniParche.Domain.Helpers;
using UniParche.API.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add OpenAPI
builder.Services.AddOpenApi();

// Add DbContext
builder.Services.AddDbContext<UniParcheDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ========== Add Repositories ==========
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventAttendeeRepository, EventAttendeeRepository>();
builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// ========== Add Services ==========
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventAttendeeService, EventAttendeeService>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupMemberService, GroupMemberService>();

// Add Helpers
builder.Services.AddScoped<UserValidationHelper>();
builder.Services.AddScoped<PostValidationHelper>();

// Add Logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();