using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.UserDto
{
    public class UserContentCountDto
    {
        public string Username { get; set; } = "";
        public int ContentCount { get; set; }
    }
}
