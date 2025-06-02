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
    public class VideoRepo : Repository<Video>, IRepository<Video>
    {
        public VideoRepo(MainDbContext ctx) : base(ctx)
        {
        }

        public override IQueryable<Video> ReadAll()
        {
            return ctx.Videos.ToList().AsQueryable().Include(v => v.Owner);
        }
        public override Video Read(string id)
        {
            return ctx.Videos.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Video item)
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
