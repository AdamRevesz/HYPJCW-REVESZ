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
        public Category CourseCategory { get; set; }

        public Course(string contentId, string title, string body, string ownerId, User owner, DateOnly createDate, int numberOfLikes, List<Comments> comments, string category) : base(contentId, title, body, ownerId, owner, createDate, numberOfLikes, comments, category)
        {
        }
        
        public Course()
        {

        }
    }
}
