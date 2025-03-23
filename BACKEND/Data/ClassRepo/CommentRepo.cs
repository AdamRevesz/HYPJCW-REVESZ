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
    public class CommentRepo : Repository<Comments>, IRepository<Comments>
    {
        public CommentRepo(MainDbContext ctx): base(ctx)
        {
        }

        public override Comments Read(string id)
        {
            return ctx.Comments.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Comments item)
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
