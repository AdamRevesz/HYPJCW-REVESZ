using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClassRepos
{
    public class ContentRepo : Repository<Content>, IRepository<Content>
    {
        public ContentRepo(MainDbContext ctx) : base(ctx)
        {

        }

        public override Content Read(string id)
        {
            return ctx.Contents.FirstOrDefault(x => x.ContentId == id) ?? new Content();
        }

        public override void Update(Content item)
        {
            var old = Read(item.ContentId);
            foreach ( var prop in old.GetType().GetProperties())
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
