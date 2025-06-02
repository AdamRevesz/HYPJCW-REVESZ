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
    public class ContentRepo : Repository<Content>, IRepository<Content>
    {
        public ContentRepo(MainDbContext ctx): base(ctx)
        {
        }

        public override IQueryable<Content> ReadAll()
        {
            return ctx.Content.ToList().AsQueryable().Include(c => c.Owner);
        }

        public override Content Read(string id)
        {
            return ctx.Content.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Content item)
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
