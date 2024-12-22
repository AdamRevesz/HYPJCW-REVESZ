using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClassRepos
{
    public class SalesItemRepo : Repository<SalesItem>, IRepository<SalesItem>
    {
        public SalesItemRepo(MainDbContext ctx) : base(ctx)
        {

        }

        public override SalesItem Read(string id)
        {
            return ctx.SalesItems.FirstOrDefault(x => x.ContentId == id) ?? new SalesItem();
        }

        public override void Update(SalesItem item)
        {
            var old = Read(item.ContentId);
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
