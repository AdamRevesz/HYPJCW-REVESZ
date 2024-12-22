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
    public class ContentLogic : IContentLogic
    {
        IRepository<Content> contentRepo;
        
        public ContentLogic(IRepository<Content> contentRepo)
        {
            this.contentRepo = contentRepo;
        }
    }
}
