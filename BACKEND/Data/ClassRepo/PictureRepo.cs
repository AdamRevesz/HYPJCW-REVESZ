using Data.Repo;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ClassRepo
{
    public class PictureRepo : Repository<Picture>, IRepository<Picture>
    {
        public PictureRepo(MainDbContext ctx) : base(ctx)
        {
        }

        public override Picture Read(string id)
        {
            return ctx.Pictures.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Picture item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
                catch(Exception)
                {
                }
            }
            ctx.SaveChanges();
        }
    }
}
