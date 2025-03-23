using Data.Repo;
using Models;
using System;

namespace Logic
{
    public class RateLogic<T> where T : class
    {
        IRepository<Content> contentRepo;

        public RateLogic(IRepository<Content> contentRepo)
        {
            this.contentRepo = contentRepo;
        }

        public void Like(string id)
        {
            var item = contentRepo.Read(id);
            if (item == null)
            {
                throw new ArgumentException("Item not found");
            }

            item.NumberOfLikes++;
            contentRepo.Update(item);
        }

        public void Dislike(string id)
        {
            var item = contentRepo.Read(id);
            if (item == null)
            {
                throw new ArgumentException("Item not found");
            }

            item.NumberOfDislikes++;
            contentRepo.Update(item);
        }
    }
}