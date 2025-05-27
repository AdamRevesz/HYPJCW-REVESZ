using Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Comments : IIdEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = "";
        public string PosterId { get; set; } = ""; // Foreign key to User
        public virtual User? Owner { get; set; }
        public string Body { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Likes { get; set; }
        public string ContentId { get; set; } = ""; // Foreign key to Content
        public virtual Content? Contents { get; set; }

        
    }
}
