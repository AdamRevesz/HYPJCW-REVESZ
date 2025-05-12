using Models.Helpers;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data.Repo;

namespace Repository
{
    

    public abstract class Repository<T>  : IRepository<T> where T : class
    {
        protected MainDbContext ctx;

        public Repository(MainDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T entity)
        {
            if (entity is IIdEntity idEntity && string.IsNullOrEmpty(idEntity.Id))
            {
                idEntity.Id = Guid.NewGuid().ToString();
            }
            ctx.Set<T>().Add(entity);
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>().Include("Owner");
        }
        public void Remove(string id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public abstract T Read(string id);
        public abstract void Update(T item);
    }
}
