using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Dtos.Content
{
    public class ContentShortViewDto
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string ApprovalRate { get; set; } = "";
        public Models.User Owner { get; set; } = new Models.User();
        public string Category { get; set; } = "";
    }
}
