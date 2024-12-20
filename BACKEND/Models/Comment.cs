using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Comment
    {
        [Key]
        public string CommentId { get; set; }
        public User Poster { get; set; }
        public string Body { get; set; }
        public int Likes { get; set; }
        public virtual User User { get; set; }
        public Content Content { get; set; }
    }
}
