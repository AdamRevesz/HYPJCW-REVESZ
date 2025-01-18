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
        public Content(string? title, string? body, string? ownerId, User? owner, DateOnly createDate, int numberOfLikes, List<Comments>? comments, string? category)
        {
            ContentId = Guid.NewGuid().ToString();
            Title = title;
            Body = body;
            OwnerId = ownerId;
            Owner = owner;
            CreateDate = createDate;
            Category = category;
        }

        [Key]
        public string? ContentId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? OwnerId { get; set; } // Foreign key to User
        public virtual User? Owner { get; set; }
        public DateOnly CreateDate { get; set; }
        public int NumberOfLikes { get; set; }
        public virtual List<Comments>? Comments { get; set; }
        [MaxLength(30)]
        public string? Category { get; set; }

    }
}
