using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SalesItem : Content
    {
        public string Type { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public int Number { get; set; }

        public SalesItem()
        {

        }
    }
}
