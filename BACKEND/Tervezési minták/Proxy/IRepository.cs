using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Proxy
{
    public interface IRepository
    {
        void AddName(string name);
        void RemoveName(string name);
        string[] ListNames();
    }


}
