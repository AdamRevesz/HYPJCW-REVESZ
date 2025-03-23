using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(string id);
        void Create(T item);
        void Update(T item);
        void Remove(string id);
    }
}
