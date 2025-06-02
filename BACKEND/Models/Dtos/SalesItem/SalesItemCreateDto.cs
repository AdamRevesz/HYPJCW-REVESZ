using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.SalesItem
{
    public class SalesItemCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = "";
        public string Body { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public string Type { get; set; }
        public bool InStock { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
    }
}
