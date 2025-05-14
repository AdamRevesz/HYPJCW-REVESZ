// SalesItemCreateUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.SalesItem
{
    public class SalesItemCreateUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; }

        [Required]
        public int Price { get; set; }

        public bool InStock { get; set; }

        public int Number { get; set; }

    }
}

