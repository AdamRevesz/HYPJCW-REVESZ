using Data.ClassRepo;
using Data.Repo;
using Logic.Helper;
using Models;
using Models.Dtos.Content;
using Models.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class FilterLogics
    {
        DtoProvider dtoProvider;
        IRepository<Content> contentRepo;
        IRepository<User> userRepo;
        IRepository<Comments> commentRepo;

        public FilterLogics(IRepository<Content> contentRepo, DtoProvider dtoProvider, IRepository<User> userRepo, IRepository<Comments> commentRepo)
        {
            this.contentRepo = contentRepo;
            this.dtoProvider = dtoProvider;
            this.userRepo = userRepo;
            this.commentRepo = commentRepo;
        }

        public IEnumerable<ContentShortViewDto> GetContentByCategory(string category)
        {
            var contentList = contentRepo.ReadAll()
                .Where(c => c.Category == category)
                .ToList();
            return contentList
                .Select(x => dtoProvider.Mapper.Map<ContentShortViewDto>(x))
                .ToList();
        }

        public IEnumerable<ContentShortViewDto> GetHighestApprovedContents()
        {
            var contentList = contentRepo.ReadAll()
                .OrderByDescending(c => c.NumberOfLikes)
                .ThenBy(c => c.NumberOfDislikes)
                .ThenByDescending(c => c.Views)
                .Select(dtoProvider.Mapper.Map<ContentShortViewDto>)
                .OrderByDescending(c => c.ApprovalRate)
                .ToList();
            return contentList;
        }

        public ContentShortViewDto GetContentHighestRatedContentOfUser(string username)
        {
            var contentList = contentRepo.ReadAll()
                .Where(c => c.Owner.UserName == username)
                .OrderByDescending(c => c.NumberOfLikes)
                .ThenBy(c => c.NumberOfDislikes)
                .ThenByDescending(c => c.Views)
                .Select(dtoProvider.Mapper.Map<ContentShortViewDto>)
                .OrderByDescending(c => c.ApprovalRate)
                .FirstOrDefault();
            return contentList;
        }

        public Dictionary<string,ContentShortViewDto> GetHighestApprovedCreator()
        {
            var grouped = contentRepo.ReadAll()
                .Where(c => c.Owner != null && !string.IsNullOrEmpty(c.Owner.UserName))
                .GroupBy(c => c.Owner.UserName)
                .ToDictionary(
                g => g.Key,
                g =>
                {
                    var bestContent = g
                    .OrderByDescending(c =>
                    (c.NumberOfLikes + c.NumberOfDislikes) > 0
                    ? (double)c.NumberOfLikes * 100 / (c.NumberOfLikes + c.NumberOfDislikes)
                    : 0)
                    .First();

                    var dto = dtoProvider.Mapper.Map<ContentShortViewDto>(bestContent);

                    string title = bestContent.Title;
                    int likes = bestContent.NumberOfLikes;
                    int dislikes = bestContent.NumberOfDislikes;
                    int total = likes + dislikes;
                    dto.ApprovalRate = total > 0
                        ? (likes * 100 / (double)(likes + dislikes)).ToString("F2")
                        : "0%";
                    return dto;
                });
                 var highestApprovedCreator = grouped
                     .OrderByDescending(c => double.Parse(c.Value.ApprovalRate.TrimEnd('%')))
                     .FirstOrDefault();
            return new Dictionary<string, ContentShortViewDto> { { highestApprovedCreator.Key, highestApprovedCreator.Value } };
        }
    }
}
