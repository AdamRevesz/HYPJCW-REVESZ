using Logic.Helper;
using Logic.Interfaces;
using Models;
using Models.Dtos.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repo;
using Data.ClassRepo;

namespace Logic
{
    public class CommentLogic
    {
        IRepository<Comments> commentRepo;
        DtoProvider dtoProvider;


        public CommentLogic(IRepository<Comments> commentRepo, DtoProvider dtoProvider)
        {
            this.commentRepo = commentRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddComment(string contentId, CommentCreateUpdateDto dto)
        {
            Comments comment = dtoProvider.Mapper.Map<Comments>(dto);
            comment.ContentId = contentId;
            comment.PosterId = "2811c75d-ae64-44f4-90f7-32aa02bd2202";
            commentRepo.Create(comment);
        }

        public async Task<IEnumerable<CommentViewDto>> GetAllComments(string contentId)
        {
            var comments = commentRepo
                .ReadAll()
                .Where(c => c.ContentId == contentId)
                .ToList();

            return await dtoProvider.MapCommentsToDtosAsync(comments);
        }

        public void DeleteComment(string id)
        {
            var comment = commentRepo.Read(id);
            commentRepo.Remove(id);           
        }

        public void UpdateComment(string id,string userId, CommentCreateUpdateDto dto)
        {
            var oldComment = commentRepo.Read(id);
            if(oldComment.PosterId != userId)
            {
                throw new ArgumentException("You are not the owner of the comment");
            }
            dtoProvider.Mapper.Map(dto, oldComment);
            commentRepo.Update(oldComment);
        }
    }
}


