using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Video : Content
    {
        public int Width { get; set; }
        public int Height { get; set; }
        [NotMapped]
        public string Resolution => $"{Width}x{Height}";

        public Video()
        {

        }
    }
}
