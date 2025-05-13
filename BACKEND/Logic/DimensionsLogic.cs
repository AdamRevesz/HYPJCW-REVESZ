using Data.Repo;
using Logic.Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Logic
{
    public class DimensionsLogic
    {
        public int[] CheckDimensions(string imagePath)
        {
            Bitmap bitmap = new Bitmap(imagePath);
            int[] dimensions = new int[2];
            dimensions[0] = bitmap.Width;
            dimensions[1] = bitmap.Height;
            return dimensions;
        }
    }
}
