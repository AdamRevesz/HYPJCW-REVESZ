using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsProfessional { get; set; }
        public DateOnly AccountCreated { get; set; }
        public virtual List<Content> Contents { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
