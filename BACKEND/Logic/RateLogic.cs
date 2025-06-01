using Data.Repo;
using Models;
using System;

namespace Logic
{
    public class RateLogic<T> where T : class
    {
        IRepository<Content> contentRepo;
        IRepository<User> userRepo;

        public RateLogic(IRepository<Content> contentRepo, IRepository<User> userRepo)
        {
            this.contentRepo = contentRepo;
            this.userRepo = userRepo;
        }

        public void Like(string id, string userId)
        {
            var user = userRepo.Read(userId);
            var item = contentRepo.Read(id);
            if (user == null || item == null)
            {
                throw new ArgumentException("User or item not found");
            }
            if(item.LikedByUsers.Any(u => u.Id == userId))
            {
                item.NumberOfLikes--;
                item.LikedByUsers.RemoveAll(u => u.Id == userId);
                user.LikedContents.RemoveAll(i => i.Id == item.Id);
                contentRepo.Update(item);
                return;
            }
            if(item.DislikedByUsers.Any(u => u.Id == userId))
            {
                item.NumberOfDislikes--;
                item.DislikedByUsers.RemoveAll(u => u.Id == userId);
                user.DislikedContents.RemoveAll(i => i.Id == item.Id);
                contentRepo.Update(item);
            }
            item.NumberOfLikes++;
            item.LikedByUsers.Add(user);
            user.LikedContents.Add(item);
            contentRepo.Update(item);
        }

        public void Dislike(string id, string userId)
        {
            var item = contentRepo.Read(id);
            var user = userRepo.Read(userId);
            if (item == null || user == null)
            {
                throw new ArgumentException("Item not found");
            }
            if(item.DislikedByUsers.Any(u => u.Id == userId))
            {
                item.NumberOfDislikes--;
                item.DislikedByUsers.RemoveAll(u => u.Id == userId);
                user.DislikedContents.RemoveAll(i => i.Id == item.Id);
                contentRepo.Update(item);
                return;
            }
            if(item.LikedByUsers.Any(u => u.Id == userId))
            {
                item.NumberOfLikes--;
                item.LikedByUsers.RemoveAll(u => u.Id == userId);
                user.LikedContents.RemoveAll(c => c.Id == item.Id);
                contentRepo.Update(item);
            }
            item.NumberOfDislikes++;
            item.DislikedByUsers.Add(user);
            user.DislikedContents.Add(item);
            contentRepo.Update(item);
        }
    }
}