using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Bridge
{
    public abstract class Shape
    {
        public abstract void Draw();
        public abstract ConsoleColor Color { get; }
    }
}
