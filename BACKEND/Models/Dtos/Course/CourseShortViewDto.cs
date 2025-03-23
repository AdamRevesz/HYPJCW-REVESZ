// CourseShortViewDto.cs
using Models.Dtos.UserDto;

namespace Models.Dtos.Course
{
    public class CourseShortViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ApprovalRate { get; set; } = string.Empty;
        public UserShortViewDto Owner { get; set; } = new UserShortViewDto();
        public string Category { get; set; } = string.Empty;
        public int Price { get; set; }
        public Category CourseCategory { get; set; }
    }
}

