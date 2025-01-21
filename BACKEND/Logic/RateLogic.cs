using Models;
using Repository;
using System;

namespace Logic
{
    public class RateLogic<T> where T : class
    {
        private readonly Repository<Content> contentRepo;

        public RateLogic(Repository<Content> contentRepo)
        {
            this.contentRepo = contentRepo;
        }

        public void Like(string id)
        {
            var item = contentRepo.FindById(id);
            if (item == null)
            {
                throw new ArgumentException("Item not found");
            }

            item.NumberOfLikes++;
            contentRepo.Update(item);
        }

        public void Dislike(string id)
        {
            var item = contentRepo.FindById(id);
            if (item == null)
            {
                throw new ArgumentException("Item not found");
            }

            item.NumberOfDislikes++;
            contentRepo.Update(item);
        }
    }
}