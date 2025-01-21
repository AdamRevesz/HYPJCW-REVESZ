using Logic.Helper;
using Logic.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repository;
using Models.Dtos.Content;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Logic
{
    public class ContentLogic
    {
        Repository<Content> contentRepo;
        DtoProvider dtoProvider;
        
        public ContentLogic(Repository<Content> contentRepo, DtoProvider dtoProvider)
        {
            this.contentRepo = contentRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddContent(ContentCreateUpdateDto dto)
        {
            Content m = dtoProvider.Mapper.Map<Content>(dto);

            if (contentRepo.GetAll().FirstOrDefault(x => x.Title == m.Title) == null)
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
            return contentRepo.GetAll().Select(x => dtoProvider.Mapper.Map<ContentShortViewDto>(x)
            );
        }

        public void DeleteContent(string id)
        {
            contentRepo.DeleteById(id);
        }

        public void DeleteOwnerContent(string id, string userId)
        {
            var content = contentRepo.FindById(id);
            if (content.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this content.");
            }
            contentRepo.DeleteById(id);
        }

        public void UpdateContent(string id,string userId,ContentCreateUpdateDto dto)
        {
            var old = contentRepo.FindById(id);
            if (old.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this content.");
            }
            dtoProvider.Mapper.Map(dto, old);
            contentRepo.Update(old);
        }

        public ContentViewDto GetContent(string id)
        {
            var model = contentRepo.FindById(id);
            return dtoProvider.Mapper.Map<ContentViewDto>(model);
        }
        
    }
}
