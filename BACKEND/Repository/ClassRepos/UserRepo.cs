using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClassRepos
{
    public class UserRepo : Repository<User>, IRepository<User>
    {
        public UserRepo(MainDbContext ctx) : base(ctx)
        {

        }

        public override User Read(string id)
        {
            return ctx.Users.FirstOrDefault(x => x.UserId == id) ?? new User();
        }

        public override void Update(User item)
        {
            var old = Read(item.UserId);
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
