using Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Content : IIdEntity
    {

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FilePath { get; set; } = "";
        [StringLength(50)]
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string OwnerId { get; set; } = ""; // Foreign key to User
        public virtual User Owner { get; set; } = new User();
        public DateOnly CreateDate { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        [NotMapped]
        public virtual List<Comments> Comments { get; set; } = new List<Comments>();
        [MaxLength(30)]
        public string Tags { get; set; } = "";


        public Content(string contentId, string title, string body, string ownerId, User owner, DateOnly createDate, int numberOfLikes, List<Comments> comments, string tags)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Body = body;
            OwnerId = ownerId;
            Owner = owner;
            CreateDate = createDate;
            Tags = tags;
            Comments = new List<Comments>();
        }
        public Content()
        {

        }
        
    }
}
