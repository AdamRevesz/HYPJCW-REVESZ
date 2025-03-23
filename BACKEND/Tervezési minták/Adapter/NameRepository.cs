using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Adapter
{
    public class NameRepository : INameSource
    {
        public string[] Names
        {
            get
            {
                return new string[] { "John", "Jack", "Jimmy", "Peter", "Tony" };
            }
        }
    }
}
