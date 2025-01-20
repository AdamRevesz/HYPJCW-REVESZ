// CourseViewDto.cs
using System.Collections.Generic;

namespace Models.Dtos.Course
{
    public class CourseViewDto
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Category { get; set; }
        public int Likes { get; set; }
        public List<Comments>? Comments { get; set; }
        public int Price { get; set; }
        public Category CourseCategory { get; set; }
    }
}

