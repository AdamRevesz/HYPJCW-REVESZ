using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Course
{
    public class CourseCreateUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int Width { get; set; } = 0;
        [Required]
        public int Height { get; set; } = 0;

        [Required]
        public string Body { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public int Price { get; set; }

        [Required]
        public Category CourseCategory { get; set; }
        [Required]
        public string OwnerId { get; set; }
    }
}

