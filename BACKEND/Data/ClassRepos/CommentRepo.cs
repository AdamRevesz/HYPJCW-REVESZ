using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClassRepos
{
    public class CommentRepo : Repository<Comments>, IRepository<Comments>
    {
        public CommentRepo(MainDbContext ctx) : base(ctx)
        {

        }
        public override Comments Read(string id)
        {
            return ctx.Comments.FirstOrDefault(x => x.CommentId == id) ?? new Comments();
        }

        public override void Update(Comments item)
        {
            var old = Read(item.CommentId);
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
