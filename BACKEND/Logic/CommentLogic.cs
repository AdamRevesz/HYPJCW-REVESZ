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
        IRepository<Comments> commentRepo;
        public CommentLogic(IRepository<Comments> commentRepo)
        {
            this.commentRepo = commentRepo;
        }
    }
}
