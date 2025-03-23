using Logic.Helper;
using Logic.Interfaces;
using Models;
using Models.Dtos.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repo;

namespace Logic
{
    public class CourseLogic
    {
        IRepository<Course> courseRepo;
        DtoProvider dtoProvider;

        public CourseLogic(IRepository<Course> courseRepo, DtoProvider dtoProvider)
        {
            this.courseRepo = courseRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddCourse(CourseCreateUpdateDto dto)
        {
            Course course = dtoProvider.Mapper.Map<Course>(dto);

            if (courseRepo.ReadAll().FirstOrDefault(x => x.Title == course.Title) == null)
            {
                courseRepo.Create(course);
            }
            else
            {
                throw new ArgumentException("Course with the same name already exists");
            }
        }

        public IEnumerable<CourseShortViewDto> GetAllCourses()
        {
            return courseRepo.ReadAll().Select(x => dtoProvider.Mapper.Map<CourseShortViewDto>(x));
        }

        public void DeleteCourse(string id)
        {
            courseRepo.Remove(id);
        }

        public void DeleteOwnerCourse(string id, string userId)
        {
            var course = courseRepo.Read(id);
            if (course.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this course.");
            }
            courseRepo.Remove(id);
        }

        public void UpdateCourse(string id, CourseCreateUpdateDto dto, string userId)
        {
            var oldCourse = courseRepo.Read(id);
            if (oldCourse.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this course.");
            }
            dtoProvider.Mapper.Map(dto, oldCourse);
            courseRepo.Update(oldCourse);
        }

        public CourseViewDto GetCourse(string id)
        {
            var course = courseRepo.Read(id);
            return dtoProvider.Mapper.Map<CourseViewDto>(course);
        }
    }
}

