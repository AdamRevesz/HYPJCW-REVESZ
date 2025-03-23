using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Proxy
{
    public class RepositoryProxy : IRepository
    {
        IRepository system;
        public RepositoryProxy(IRepository system)
        {
            this.system = system;
        }
        public void AddName(string name)
        {
            File.AppendAllText("log_c.txt", DateTime.Now.ToShortDateString());
            system.AddName(name);
        }
        public string[] ListNames()
        {
            return system.ListNames();
        }
        public void RemoveName(string name)
        {
            File.AppendAllText("log_d.txt", DateTime.Now.ToShortDateString());
            system.RemoveName(name);
        }
    }
}
