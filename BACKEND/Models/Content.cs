using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Content
    {
        [Key]
        public string ContentId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string OwnerId { get; set; } // Foreign key to User
        public virtual User Owner { get; set; }
        public DateOnly CreateDate { get; set; }
        public int NumberOfLikes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        [MaxLength(30)]
        public string Category { get; set; }
    }
}
