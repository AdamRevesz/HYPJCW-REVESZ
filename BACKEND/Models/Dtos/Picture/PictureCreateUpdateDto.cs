// PictureCreateUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Picture
{
    public class PictureCreateUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = "";
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        [Required]
        public string Body { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        public string OwnerId { get; set; }
    }
}
