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
    public class CourseRepo : Repository<Course>, IRepository<Course>
    {
        public CourseRepo(MainDbContext ctx): base(ctx) 
        { 
        }

        public override Course Read(string id)
        {
            if (id == null)
            {
                throw new Exception("Invalid input");
            }
            return ctx.Courses.FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Course item)
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
