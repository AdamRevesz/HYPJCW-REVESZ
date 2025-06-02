using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Comment
{
    public class CommentViewDto
    {
        public string Username { get; set; } = "";
        public string Body { get; set; } = "";
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Id { get; set; } = "";
    }
}
