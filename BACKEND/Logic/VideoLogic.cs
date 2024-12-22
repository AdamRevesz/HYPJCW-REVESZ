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
    public class VideoLogic : IVideoLogic
    {
        IRepository<Video> videoRepo;
        public VideoLogic(IRepository<Video> videoRepo)
        {
            this.videoRepo = videoRepo;
        }
    }
}
