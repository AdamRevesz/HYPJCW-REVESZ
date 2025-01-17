using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Content
{
    public class ContentShortViewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public User Owner { get; set; }
        public string Category { get; set; }
    }
}
