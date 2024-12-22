using Logic.Interfaces;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CommentLogic : ICommentLogic
    {
        IRepository<Comment> commentRepo;
        public CommentLogic(IRepository<Comment> commentRepo)
        {
            this.commentRepo = commentRepo;
        }
    }
}
