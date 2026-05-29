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
        // ========== User Mappings ==========

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
            .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src =>
                src.University != null ? src.University.Name : string.Empty));

        // ========== University Mappings ==========

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
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.MinValue))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.MinValue));

        // ========== Post Mappings ==========

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
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.IsLikedByCurrentUser, opt => opt.Ignore());

        // ========== Comment Mappings ==========

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
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.UserProfilePictureUrl, opt => opt.MapFrom(src =>
                src.User != null ? src.User.ProfilePictureUrl : string.Empty))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        // ========== Like Mappings ==========

        CreateMap<CreateLikeRequest, Like>()
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.ReactionType, opt => opt.MapFrom(src => src.ReactionType))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Post, opt => opt.Ignore());

        CreateMap<Like, LikeResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.ReactionType, opt => opt.MapFrom(src => src.ReactionType));

        // ========== Event Mappings ==========

        CreateMap<CreateEventRequest, Event>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            //.ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            //La siguiente linea se agrego por Felipe (1 Linea)
            .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.CreatorId))
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Creator, opt => opt.Ignore())
            .ForMember(dest => dest.University, opt => opt.Ignore())
            .ForMember(dest => dest.EventAttendees, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateEventRequest, Event>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.UniversityId, opt => opt.Ignore())
            .ForMember(dest => dest.Creator, opt => opt.Ignore())
            .ForMember(dest => dest.University, opt => opt.Ignore())
            .ForMember(dest => dest.EventAttendees, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Event, EventResponse>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.CreatorId))
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src =>
                src.Creator != null ? src.Creator.UserName : string.Empty))
            .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
            .ForMember(dest => dest.AttendeeCount, opt => opt.MapFrom(src =>
                src.EventAttendees != null ? src.EventAttendees.Count : 0))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        // ========== EventAttendee Mappings ==========

        CreateMap<EventAttendee, EventAttendeeResponse>()
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
            .ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src =>
                src.Event != null ? src.Event.Title : string.Empty))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

        // ========== Group Mappings ==========

        CreateMap<CreateGroupRequest, Group>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.Creator, opt => opt.Ignore())
            .ForMember(dest => dest.University, opt => opt.Ignore())
            .ForMember(dest => dest.Members, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateGroupRequest, Group>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.UniversityId, opt => opt.Ignore())
            .ForMember(dest => dest.Creator, opt => opt.Ignore())
            .ForMember(dest => dest.University, opt => opt.Ignore())
            .ForMember(dest => dest.Members, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Group, GroupResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.CreatorId))
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src =>
                src.Creator != null ? src.Creator.UserName : string.Empty))
            .ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
            .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(src =>
                src.Members != null ? src.Members.Count : 0))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        // ========== GroupMember Mappings ==========

        CreateMap<GroupMember, GroupMemberResponse>()
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src =>
                src.Group != null ? src.Group.Name : string.Empty))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                src.User != null ? src.User.UserName : string.Empty))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        // ========== Friendship Mappings ==========

        CreateMap<CreateFriendshipRequest, Friendship>()
            .ForMember(dest => dest.User1Id, opt => opt.MapFrom(src => src.User1Id))
            .ForMember(dest => dest.User2Id, opt => opt.MapFrom(src => src.User2Id))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.User1, opt => opt.Ignore())
            .ForMember(dest => dest.User2, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateFriendshipRequest, Friendship>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.User1Id, opt => opt.Ignore())
            .ForMember(dest => dest.User2Id, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.User1, opt => opt.Ignore())
            .ForMember(dest => dest.User2, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Friendship, FriendshipResponse>()
            .ForMember(dest => dest.User1Id, opt => opt.MapFrom(src => src.User1Id))
            .ForMember(dest => dest.User1Name, opt => opt.MapFrom(src =>
                src.User1 != null ? src.User1.UserName : string.Empty))
            .ForMember(dest => dest.User2Id, opt => opt.MapFrom(src => src.User2Id))
            .ForMember(dest => dest.User2Name, opt => opt.MapFrom(src =>
                src.User2 != null ? src.User2.UserName : string.Empty))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));
    }
}