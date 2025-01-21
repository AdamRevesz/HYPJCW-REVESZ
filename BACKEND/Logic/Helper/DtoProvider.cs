using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Dtos.Comment;
using Models.Dtos.Content;
using Models.Dtos.Course;
using Models.Dtos.Picture;
using Models.Dtos.SalesItem;
using Models.Dtos.User;
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
                    dest.ApprovalRate = src.NumberOfLikes/(src.NumberOfLikes + src.NumberOfDislikes)+"%";
                });

                cfg.CreateMap<Picture, PictureShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ApprovalRate = src.NumberOfLikes / (src.NumberOfLikes + src.NumberOfDislikes) + "%";
                });

                cfg.CreateMap<Video, VideoShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ApprovalRate = src.NumberOfLikes / (src.NumberOfLikes + src.NumberOfDislikes) + "%";
                });

                cfg.CreateMap<Course, CourseShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ApprovalRate = src.NumberOfLikes / (src.NumberOfLikes + src.NumberOfDislikes) + "%";
                });

                cfg.CreateMap<SalesItem, SalesItemShortViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ApprovalRate = src.NumberOfLikes / (src.NumberOfLikes + src.NumberOfDislikes) + "%";
                });

                cfg.CreateMap<User, UserViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.IsAdmin = userManager.IsInRoleAsync(src, "Admin").Result;
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
                cfg.CreateMap<Comments, CommentViewDto>()
                .AfterMap((src, dest) =>
                {
                    var user = userManager.Users.First(u => u.Id == src.Id);
                    dest.Username = user.UserName;
                });
            });

            Mapper = new Mapper(config);
        }
    }
}
