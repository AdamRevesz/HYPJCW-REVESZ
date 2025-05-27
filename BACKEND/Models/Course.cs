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
        public Category CourseCategory { get; set; }

        
        public Course()
        {

        }
    }
}
