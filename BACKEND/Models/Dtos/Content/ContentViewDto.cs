using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Dtos.Content
{
    public class ContentViewDto
    {
        public string? Id { get; set; } = "";
        public string? FilePath { get; set; } = "";
        public string? Title { get; set; }
        public string? Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public List<Comments>? Comment { get; set; }
    }
}
