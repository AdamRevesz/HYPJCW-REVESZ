using Logic.Helper;
using Logic.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Logic
{
    public class ContentLogic : IContentLogic
    {
        Repository<Content> contentRepo;
        
        public ContentLogic(Repository<Content> contentRepo)
        {
            this.contentRepo = contentRepo;
        }
        
    }
}
