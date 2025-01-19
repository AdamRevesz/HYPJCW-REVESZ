using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClassRepos
{
    public class PictureRepo : Repository<Picture>, IRepository<Picture>
    {
        public PictureRepo(MainDbContext ctx) : base(ctx)
        {

        }

        public override Picture Read(string id)
        {
            return ctx.Pictures.FirstOrDefault(x => x.ContentId == id) ?? new Picture();
        }

        public override void Update(Picture item)
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
