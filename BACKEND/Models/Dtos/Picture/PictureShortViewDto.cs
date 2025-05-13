// PictureShortViewDto.cs
using Models.Dtos.User;
using Models.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Picture
{
    public class PictureShortViewDto
    {
        public string Id { get; set; } = string.Empty;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string ApprovalRate { get; set; } = string.Empty;
        public UserShortViewDto Owner { get; set; } = new UserShortViewDto();
        public string Category { get; set; } = string.Empty;
        public string Resolution { get; set; } = string.Empty;
    }
}
