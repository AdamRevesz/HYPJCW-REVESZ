using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Adapter
{
    public interface INameData
    {
        public Queue<string> DataQueue { get; }
    }
}
