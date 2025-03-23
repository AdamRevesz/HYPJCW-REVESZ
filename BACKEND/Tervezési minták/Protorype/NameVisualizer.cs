using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tervezési_minták.Adapter;

namespace Tervezési_minták.Protorype
{
    public class NameVisualizer
    {
        INameData data;
        public NameVisualizer(INameData data)
        {
            this.data = data;
        }
        public void Show()
        {
            while (data.DataQueue.Count > 0)
            {
                Console.WriteLine("***" + data.DataQueue.Dequeue() + "***");
            }
        }
    }
}
