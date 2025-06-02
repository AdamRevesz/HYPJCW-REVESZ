using Data.Repo;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ClassRepo
{
    public class SalesItemRepo : Repository<SalesItem>, IRepository<SalesItem>
    {
        public SalesItemRepo(MainDbContext ctx) : base(ctx)
        {
        }

        public override IQueryable<SalesItem> ReadAll()
        {
            return ctx.SalesItems.ToList().AsQueryable().Include(v => v.Owner);
        }

        public override SalesItem Read(string id)
        {
            return ctx.SalesItems.FirstOrDefault(x => x.Id == id); 
        }

        public override void Update(SalesItem item)
        {
            var old = Read(item.Id);

            foreach (var prop in old.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
                catch (Exception)
                {
                }
            }
            ctx.SaveChanges();
        }
    }
}
