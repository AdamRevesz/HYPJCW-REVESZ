using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Dtos.User;
using Models.Dtos.UserDto;

namespace Models.Dtos.Content
{
    public class ContentShortViewDto
    {
        public string Id { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string Title { get; set; } = "";
        public string ApprovalRate { get; set; } = "";
        public UserShortViewDto Owner { get; set; } = new UserShortViewDto();
        public string Category { get; set; } = "";
    }
}
