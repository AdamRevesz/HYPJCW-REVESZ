using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.User
{
    public class UserInputDto
    {
        public required string EmailAddress { get; set; }
        [MinLength(8)]
        public required string Username { get; set; } = "";
        [MinLength(8)]
        public required string Password { get; set; } = "";
        public required string FirstName { get; set; } = "";
        public required string LastName { get; set; } = "";
        public required DateOnly BirthDate { get; set; }
        public required bool IsProfessional { get; set; } = false;
    }
}
