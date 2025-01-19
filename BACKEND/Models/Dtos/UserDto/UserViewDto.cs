using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.User
{
    public class UserViewDto
    {
        public string UserId { get; set; } = "";
        public string Username { get; set; } = "";
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public bool IsProfessional { get; set; }
    }
}
