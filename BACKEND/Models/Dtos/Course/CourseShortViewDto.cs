// CourseShortViewDto.cs
namespace Models.Dtos.Course
{
    public class CourseShortViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ApprovalRate { get; set; } = string.Empty;
        public Models.User Owner { get; set; } = new Models.User();
        public string Category { get; set; } = string.Empty;
        public int Price { get; set; }
        public Category CourseCategory { get; set; }
    }
}

