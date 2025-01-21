using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Content
{
    public class ContentCreateDto
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required string Tags { get; set; }
        public string OwnerId { get; set; } = "";
    }
}
