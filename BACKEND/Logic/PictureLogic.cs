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
    public class PictureLogic : IPictureLogic
    {
        IRepository<Picture> pictureRepo;
        public PictureLogic(IRepository<Picture> pictureRepo)
        {
            this.pictureRepo = pictureRepo;
        }
    }
}
