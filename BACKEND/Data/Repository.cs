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

namespace Repository
{
    

    public class Repository<T> where T : class, IIdEntity
    {
        private readonly MainDbContext _ctx;

        public Repository(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(T entity)
        {
            _ctx.Set<T>().Add(entity);
            _ctx.SaveChanges();
        }

        public T FindById(string id)
        {
            return _ctx.Set<T>().First(t => t.Id == id);
        }

        public void DeleteById(string id)
        {
            var entity = FindById(id);
            _ctx.Set<T>().Remove(entity);
            _ctx.SaveChanges();
        }

        public void Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            _ctx.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _ctx.Set<T>();
        }

        public void Update(T entity)
        {
            var old = FindById(entity.Id);
            foreach (var prop in typeof(T).GetProperties())
            {
                prop.SetValue(old, prop.GetValue(entity));
            }
            _ctx.Set<T>().Update(old);
            _ctx.SaveChanges();
        }
    }
}
