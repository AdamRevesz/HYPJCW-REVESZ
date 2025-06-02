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
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        [StringLength(50)]
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public int Price { get; set; } = 0;
        public string OwnerId { get; set; } = ""; // Foreign key to User
        public virtual User? Owner { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public int Views { get; set; }
        [NotMapped]
        public virtual List<Comments> Comments { get; set; } = new List<Comments>();
        [MaxLength(30)]
        public string Tags { get; set; } = "";
        public virtual List<User> LikedByUsers { get; set; } = new List<User>();
        public virtual List<User> DislikedByUsers { get; set; } = new List<User>();


        public Content(string contentId, string title, string body, string ownerId, User owner, int numberOfLikes, List<Comments> comments, string tags)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Body = body;
            OwnerId = ownerId;
            Owner = owner;
            Tags = tags;
            Comments = new List<Comments>();
        }
        public Content()
        {

        }
        
    }
}
