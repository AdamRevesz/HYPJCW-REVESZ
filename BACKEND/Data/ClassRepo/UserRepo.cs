using Data.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ClassRepo
{
    public class UserRepo : Repository<User>, IRepository<User>
    {
        public UserRepo(MainDbContext ctx): base(ctx)
        {
        }

        public override IQueryable<User> ReadAll()
        {
            return ctx.Users.ToList().AsQueryable(); ;
        }

        public override User Read(string id)
        {
            return ctx.User.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(User item)
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
