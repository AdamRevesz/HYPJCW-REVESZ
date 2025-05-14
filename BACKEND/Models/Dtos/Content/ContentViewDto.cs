using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Dtos.Content
{
    public class ContentViewDto
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int Likes { get; set; }
        public List<Comments>? Comment { get; set; }
    }
}
