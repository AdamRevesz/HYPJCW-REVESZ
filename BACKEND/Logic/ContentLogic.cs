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

namespace Logic
{
    public class ContentLogic
    {
        IRepository<Content> contentRepo;
        DtoProvider dtoProvider;
        
        public ContentLogic(IRepository<Content> contentRepo, DtoProvider dtoProvider)
        {
            this.contentRepo = contentRepo;
            this.dtoProvider = dtoProvider;
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

        public void UpdateContent(string id,string userId,ContentCreateDto dto)
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
        
    }
}
