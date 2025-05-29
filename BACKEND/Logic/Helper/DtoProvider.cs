using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dtos.Comment;
using Models.Dtos.Content;
using Models.Dtos.Course;
using Models.Dtos.Picture;
using Models.Dtos.SalesItem;
using Models.Dtos.User;
using Models.Dtos.UserDto;
using Models.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class DtoProvider
    {
        UserManager<User> userManager;

        public Mapper Mapper { get; }

        public DtoProvider(UserManager<User> userManager)
        {
            this.userManager = userManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Content, ContentShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    if (src.NumberOfLikes + src.NumberOfDislikes == 0)
                    {
                        dest.ApprovalRate = "N/A";
                    }
                    else
                    {
                        dest.ApprovalRate = (src.NumberOfLikes / (double)(src.NumberOfLikes + src.NumberOfDislikes)).ToString("P0");
                    }
                });

                cfg.CreateMap<Picture, PictureShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    if (src.NumberOfLikes + src.NumberOfDislikes == 0)
                    {
                        dest.ApprovalRate = "N/A";
                    }
                    else
                    {
                        dest.ApprovalRate = (src.NumberOfLikes / (double)(src.NumberOfLikes + src.NumberOfDislikes)).ToString("P0");
                    }
                });

                cfg.CreateMap<Video, VideoShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    if (src.NumberOfLikes + src.NumberOfDislikes == 0)
                    {
                        dest.ApprovalRate = "N/A";
                    }
                    else
                    {
                        dest.ApprovalRate = (src.NumberOfLikes / (double)(src.NumberOfLikes + src.NumberOfDislikes)).ToString("P0");
                    }
                });

                cfg.CreateMap<Course, CourseShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    if (src.NumberOfLikes + src.NumberOfDislikes == 0)
                    {
                        dest.ApprovalRate = "N/A";
                    }
                    else
                    {
                        dest.ApprovalRate = (src.NumberOfLikes / (double)(src.NumberOfLikes + src.NumberOfDislikes)).ToString("P0");
                    }
                });

                cfg.CreateMap<SalesItem, SalesItemShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    if (src.NumberOfLikes + src.NumberOfDislikes == 0)
                    {
                        dest.ApprovalRate = "N/A";
                    }
                    else
                    {
                        dest.ApprovalRate = (src.NumberOfLikes / (double)(src.NumberOfLikes + src.NumberOfDislikes)).ToString("P0");
                    }
                });

                cfg.CreateMap<User, UserShortViewDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.IsProfessional, opt => opt.MapFrom(src => src.IsProfessional))
                ;

                cfg.CreateMap<User, UserViewDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.IsProfessional, opt => opt.MapFrom(src => src.IsProfessional));
                ;

                cfg.CreateMap<Content, ContentShortViewDto>()
                    .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                    .AfterMap((src, dest) =>
                    {
                        double totalVotes = src.NumberOfLikes + src.NumberOfDislikes;
                        dest.ApprovalRate = totalVotes > 0
                            ? (src.NumberOfLikes / totalVotes).ToString("P0")
                            : "N/A";
                    });
                cfg.CreateMap<Content, ContentViewDto>();
                cfg.CreateMap<ContentCreateDto, Content>();
                cfg.CreateMap<Video, VideoViewDto>();
                cfg.CreateMap<VideoCreateUpdateDto, Video>();
                cfg.CreateMap<Picture, PictureViewDto>();
                cfg.CreateMap<PictureCreateUpdateDto, Picture>();
                cfg.CreateMap<Course, CourseViewDto>();
                cfg.CreateMap<CourseCreateUpdateDto, Course>();
                cfg.CreateMap<SalesItem, SalesItemViewDto>();
                cfg.CreateMap<SalesItemCreateUpdateDto, SalesItem>();
                cfg.CreateMap<CommentCreateUpdateDto, Comments>();
                cfg.CreateMap<User, UserViewDto>();
                cfg.CreateMap<UserUpdateDto, User>();
                cfg.CreateMap<UserUpdatePictureDto, User>();
                cfg.CreateMap<Comments, CommentViewDto>()
            .ForMember(dest => dest.Username, opt => opt.Ignore());
            });

            Mapper = new Mapper(config);
        }
        public async Task<UserViewDto> MapUserToDtoAsync(User user)
        {
            var userDto = Mapper.Map<UserViewDto>(user);
            userDto.IsAdmin = await userManager.IsInRoleAsync(user, "Admin");
            return userDto;
        }
        public async Task<List<CommentViewDto>> MapCommentsToDtosAsync(List<Comments> comments)
        {
            // Pre-fetch user data
            var userIds = comments.Select(c => c.PosterId).Distinct().ToList();
            var users = await userManager.Users.AsNoTracking()
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.UserName);

            // Map comments to DTOs
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comments, CommentViewDto>()
                    .AfterMap((src, dest) =>
                    {
                        dest.Username = users.ContainsKey(src.PosterId) ? users[src.PosterId] : "Unknown";
                    });
            });

            var mapper = new Mapper(config);
            return comments.Select(c => mapper.Map<CommentViewDto>(c)).ToList();
        }
        public CommentViewDto MapCommentToDto(Comments comment, Dictionary<string, string> usernameCache = null)
        {
            var dto = Mapper.Map<CommentViewDto>(comment);

            // If a username cache is provided, use it
            if (usernameCache != null && usernameCache.ContainsKey(comment.PosterId))
            {
                dto.Username = usernameCache[comment.PosterId];
            }
            else
            {
                // Otherwise look it up individually - this is less efficient
                var user = userManager.Users.AsNoTracking()
                    .FirstOrDefault(u => u.Id == comment.PosterId);
                dto.Username = user?.UserName ?? "Unknown";
            }

            return dto;
        }
    }
}
