using AutoMapper;
using UniParche.Domain.Entities;
using UniParche.API.DTOs.Request;
using UniParche.API.DTOs.Response;

namespace UniParche.API.Mappers;

/// <summary>
/// Perfil de AutoMapper para mapear entre entidades y DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
        {
            // User Mappings
            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CareerName, opt => opt.MapFrom(src => src.CareerName))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester))
                .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.Ignore())
                .ForMember(dest => dest.RegisterTime, opt => opt.Ignore())
                .ForMember(dest => dest.University, opt => opt.Ignore());

            CreateMap<UpdateUserRequest, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.CareerName, opt => opt.MapFrom(src => src.CareerName))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.UniversityId, opt => opt.Ignore())
                .ForMember(dest => dest.RegisterTime, opt => opt.Ignore())
                .ForMember(dest => dest.University, opt => opt.Ignore());

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CareerName, opt => opt.MapFrom(src => src.CareerName))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterTime))
                .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.University != null ? src.University.Name : string.Empty));

        // University Mappings
        CreateMap<CreateUniversityRequest, University>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DomainEmail, opt => opt.MapFrom(src => src.DomainEmail))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Users, opt => opt.Ignore());

        CreateMap<UpdateUniversityRequest, University>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DomainEmail, opt => opt.MapFrom(src => src.DomainEmail))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Users, opt => opt.Ignore());

        CreateMap<University, UniversityResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DomainEmail, opt => opt.MapFrom(src => src.DomainEmail))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.MinValue)) // Placeholder
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.MinValue)); // Placeholder

        // Post Mappings
        CreateMap<CreatePostRequest, Post>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<UpdatePostRequest, Post>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<Post, PostResponse>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => 0)) // Placeholder
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => 0)) // Placeholder
            .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => 0)) // Placeholder
            .ForMember(dest => dest.IsLikedByCurrentUser, opt => opt.Ignore());

        // Comment Mappings
        CreateMap<CreateCommentRequest, Comment>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Post, opt => opt.Ignore());

        CreateMap<UpdateCommentRequest, Comment>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PostId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Post, opt => opt.Ignore());

        CreateMap<Comment, CommentResponse>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.UserProfilePictureUrl, opt => opt.MapFrom(src => src.User != null ? src.User.ProfilePictureUrl : string.Empty))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        // Like Mappings
        CreateMap<CreateLikeRequest, Like>()
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.ReactionType, opt => opt.MapFrom(src => src.ReactionType))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Post, opt => opt.Ignore());

        CreateMap<Like, LikeResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.ReactionType, opt => opt.MapFrom(src => src.ReactionType));
    }
}
