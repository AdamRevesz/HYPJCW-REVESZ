using Logic.Helper;
using Logic.Interfaces;
using Models;
using Repository;
using Models.Dtos.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CommentLogic
    {
        private readonly Repository<Comments> commentRepo;
        private readonly DtoProvider dtoProvider;

        public CommentLogic(Repository<Comments> commentRepo, DtoProvider dtoProvider)
        {
            this.commentRepo = commentRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddComment(CommentCreateUpdateDto dto)
        {
            Comments comment = dtoProvider.Mapper.Map<Comments>(dto);
            commentRepo.Create(comment);
        }

        public IEnumerable<CommentViewDto> GetAllComments()
        {
            return commentRepo.GetAll().Select(x => dtoProvider.Mapper.Map<CommentViewDto>(x));
        }

        public void DeleteComment(string id)
        {
            commentRepo.DeleteById(id);
        }

        public void UpdateComment(string id, CommentCreateUpdateDto dto)
        {
            var oldComment = commentRepo.FindById(id);
            dtoProvider.Mapper.Map(dto, oldComment);
            commentRepo.Update(oldComment);
        }
    }
}


