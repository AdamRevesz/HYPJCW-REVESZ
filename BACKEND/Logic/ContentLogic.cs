using Logic.Helper;
using Logic.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Dtos.Content;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Data.Repo;
using Microsoft.IdentityModel.Tokens;

namespace Logic
{
    public class ContentLogic
    {
        IRepository<Content> contentRepo;
        DtoProvider dtoProvider;
        IRepository<User> userRepo;

        public ContentLogic(IRepository<Content> contentRepo, DtoProvider dtoProvider, IRepository<User> userRepo)
        {
            this.contentRepo = contentRepo;
            this.dtoProvider = dtoProvider;
            this.userRepo = userRepo;
        }

        public void AddContent(ContentCreateDto dto)
        {
            Content m = dtoProvider.Mapper.Map<Content>(dto);

            if (contentRepo.ReadAll().FirstOrDefault(x => x.Title == m.Title) == null)
            {
                contentRepo.Create(m);
            }
            else
            {
                throw new ArgumentException("Content with the same name already exist");
            }
        }

        public IEnumerable<ContentShortViewDto> GetAllContent()
        {
            var contentList = contentRepo.ReadAll()
                .Include(c => c.Owner)
                .ToList();

            return contentList.Select(x => dtoProvider.Mapper.Map<ContentShortViewDto>(x));
        }

        public void DeleteContent(string id)
        {
            contentRepo.Remove(id);
        }

        public void DeleteOwnerContent(string id, string userId)
        {
            var content = contentRepo.Read(id);
            if (content.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this content.");
            }
            contentRepo.Remove(id);
        }

        public void UpdateContent(string id, string userId, ContentCreateDto dto)
        {
            var old = contentRepo.Read(id);
            if (old.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this content.");
            }
            dtoProvider.Mapper.Map(dto, old);
            contentRepo.Update(old);
        }

        public ContentViewDto GetContent(string id)
        {
            var model = contentRepo.Read(id);
            return dtoProvider.Mapper.Map<ContentViewDto>(model);
        }

        public List<ContentShortViewDto> GetContentByOwner(string ownerId)
        {
            var contentList = contentRepo.ReadAll()
                .Where(c => c.OwnerId == ownerId)
                .Include(c => c.Owner)
                .ToList();
            return contentList.Select(x => dtoProvider.Mapper.Map<ContentShortViewDto>(x)).ToList();
        }
        //Made to delete data from the database for testing purposes
        public void DeleteAllContent()
        {
            var allContent = contentRepo.ReadAll().ToList();
            foreach (var content in allContent)
            {
                contentRepo.Remove(content.Id);
            }
        }

        public void LikeDislikeViewership()
        {
            var contents = contentRepo.ReadAll().ToList();
            Random r = new Random();


            foreach (var content in contents)
            {
                int randomLikesNumber = r.Next(0, 300);
                int randomDislikesNumber = r.Next(0, 300);
                int randomViewershipNumber = r.Next(0, 1000);
                content.NumberOfLikes = randomLikesNumber;
                content.NumberOfDislikes = randomDislikesNumber;
                content.Views = randomViewershipNumber;
                contentRepo.Update(content);
            }
        }

        public void RandomTag()
        {
            var contents = contentRepo.ReadAll().ToList();
            Random r = new Random();
            string[] tags = { "DigitalArt", "OilPainting", "WaterColor", "3DModel", "ConceptArt", "Illustration", "CharacterArt", "LandscapeConcept", "LandscapeIllustration" };
            foreach (var content in contents)
            {
                int randomIndex = r.Next(tags.Length);
                content.Category = tags[randomIndex];
                contentRepo.Update(content);
            }
        }

        //Random picture
        public void RandomFilePath()
        {
            var contents = contentRepo.ReadAll().ToList();
            Random r = new Random();
            string[] pictureFileNames = new[]
            {
             "pic1.jpeg",
             "pic2.jpg",
             "pic3.jpg",
             "pic4.webp",
             "pic5.png",
             "pic6.png",
             "pic7.jpg",
             "pic8.jpg",
             "pic9.webp",
             "pic10.jpg",
             "pic11.jpg",
             "pic12.jpg",
             "pic13.png",
             "pic14.jpg",
             "pic15.jpg",
             "pic16.jpg",
             "pic17.webp",
             "pic18.png",
             "pic19.gif",

            };
            foreach (var content in contents)
            {
                int randomIndex = r.Next(pictureFileNames.Length);
                content.FilePath = $"assets/UploadedPictures/{pictureFileNames[randomIndex]}";
                contentRepo.Update(content);
            }
        }

        public void DeleteAllContentWithNoExistingUserId()
        {
            var contents = contentRepo.ReadAll().ToList();
            var users = userRepo.ReadAll().ToList();
            var userIds = users.Select(u => u.Id).ToList();
            foreach (var content in contents)
            {
                if (!userIds.Contains(content.OwnerId))
                {
                    contentRepo.Remove(content.Id);
                }
            }
        }
    }
}
