using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum Category
    {
        DigitalArt,
        ClassicArt,
        MusicProduction,
        Writing,
        Gastronomy
    }

    public class Course : Content
    {
        public int Price { get; set; }
        public Category Category { get; set; }
    }
}
