using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.UserDto
{
    public class UserLoginDto
    {
        public required string Username { get; set; } = "";
        public required string Password { get; set; } = "";
    }
}
