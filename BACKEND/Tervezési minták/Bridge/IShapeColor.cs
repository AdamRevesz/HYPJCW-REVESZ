using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Bridge
{
    public interface IShapeColor
    {
        ConsoleColor Color { get; }
        string Name { get; }
    }
}
