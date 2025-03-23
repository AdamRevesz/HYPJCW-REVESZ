using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Adapter
{
    public class NameSourceToNameDataAdapter : INameData
    {
        public Queue<string> DataQueue { get; set; }
        public NameSourceToNameDataAdapter(INameSource source)
        {
            DataQueue = new Queue<string>();
            foreach (var item in source.Names)
            {
                DataQueue.Enqueue(item);
            }
        }
    }
}
