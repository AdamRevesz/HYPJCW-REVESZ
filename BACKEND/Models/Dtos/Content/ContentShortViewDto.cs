using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Dtos.User;

namespace Models.Dtos.Content
{
    public class ContentShortViewDto
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string ApprovalRate { get; set; } = "";
        public UserViewDto Owner { get; set; } = new UserViewDto();
        public string Category { get; set; } = "";
    }
}
