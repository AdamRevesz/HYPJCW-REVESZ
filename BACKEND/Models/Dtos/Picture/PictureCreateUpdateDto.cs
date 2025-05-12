// PictureCreateUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Picture
{
    public class PictureCreateUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string filePath { get; set; } = "";

        [Required]
        public string Body { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string OwnerId { get; set; }
    }
}
