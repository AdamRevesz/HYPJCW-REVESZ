// SalesItemCreateUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.SalesItem
{
    public class SalesItemCreateUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public TypeOfItem Type { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool InStock { get; set; }

        [Required]
        public int Number { get; set; }
    }
}

