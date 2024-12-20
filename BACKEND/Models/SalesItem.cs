using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum TypeOfItem
    {
        Brush,
        Model,
        Print,
        Figure,
        Templates
    }
    public class SalesItem : Content
    {
        public TypeOfItem Type { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public int Number { get; set; }
    }
}
