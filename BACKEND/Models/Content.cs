using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Content
    {
        public string ContentId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User Owner { get; set; }
        public DateOnly CreateDate { get; set; }
        public int NumberOfLikes { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
