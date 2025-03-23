using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Facade
{
    public class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Director { get; set; }

        public Movie(string title, string genre, string year, string director)
        {
            Title = title;
            Genre = genre;
            Year = year;
            Director = director;
        }

        public override string ToString()
        {
            return $"Movie name: {Title}";
        }
    }
}
