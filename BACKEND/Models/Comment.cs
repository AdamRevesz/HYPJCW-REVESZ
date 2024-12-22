using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Comment
    {
        [Key]
        public string CommentId { get; set; }
        public string PosterId { get; set; } // Foreign key to User
        public virtual User Poster { get; set; }
        public string Body { get; set; }
        public int Likes { get; set; }
        public string ContentId { get; set; } // Foreign key to Content
        public virtual Content Contents { get; set; }
    }
}
