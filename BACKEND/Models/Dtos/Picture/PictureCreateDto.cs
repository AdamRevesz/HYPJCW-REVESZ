using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Picture
{
    public class PictureCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = "";
        public string Body { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
    }
}
