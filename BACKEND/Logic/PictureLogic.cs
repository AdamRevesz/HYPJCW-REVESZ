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
    public class PictureLogic
    {
        Repository<Picture> pictureRepo;
        public PictureLogic(Repository<Picture> pictureRepo)
        {
            this.pictureRepo = pictureRepo;
        }
    }
}
