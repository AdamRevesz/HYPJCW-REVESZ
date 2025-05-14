// SalesItemViewDto.cs
using System.Collections.Generic;

namespace Models.Dtos.SalesItem
{
    public class SalesItemViewDto
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Category { get; set; }
        public int Likes { get; set; }
        public List<Comments>? Comments { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public int Number { get; set; }
    }
}

