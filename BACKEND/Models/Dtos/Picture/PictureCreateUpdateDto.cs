// PictureCreateUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Picture
{
    public class PictureCreateUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = "";

        public string Body { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
    }
}
