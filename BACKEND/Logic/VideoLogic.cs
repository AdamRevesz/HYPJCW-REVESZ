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
    public class VideoLogic
    {
        Repository<Video> videoRepo;
        public VideoLogic(Repository<Video> videoRepo)
        {
            this.videoRepo = videoRepo;
        }
    }
}
