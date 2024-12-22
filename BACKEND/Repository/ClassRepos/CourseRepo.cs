using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClassRepos
{
    public class CourseRepo : Repository<Course>, IRepository<Course>
    {
        public CourseRepo(MainDbContext ctx) : base(ctx)
        {

        }
        public override Course Read(string id)
        {
            return ctx.Courses.FirstOrDefault(x => x.ContentId == id) ?? new Course();
        }

        public override void Update(Course item)
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
