using Logic.Helper;
using Logic.Interfaces;
using Models;
using Repository;
using Models.Dtos.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CourseLogic
    {
        private readonly Repository<Course> courseRepo;
        private readonly DtoProvider dtoProvider;

        public CourseLogic(Repository<Course> courseRepo, DtoProvider dtoProvider)
        {
            this.courseRepo = courseRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddCourse(CourseCreateUpdateDto dto)
        {
            Course course = dtoProvider.Mapper.Map<Course>(dto);

            if (courseRepo.GetAll().FirstOrDefault(x => x.Title == course.Title) == null)
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
            return courseRepo.GetAll().Select(x => dtoProvider.Mapper.Map<CourseShortViewDto>(x));
        }

        public void DeleteCourse(string id)
        {
            courseRepo.DeleteById(id);
        }

        public void DeleteOwnerCourse(string id, string userId)
        {
            var course = courseRepo.FindById(id);
            if (course.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this course.");
            }
            courseRepo.DeleteById(id);
        }

        public void UpdateCourse(string id, CourseCreateUpdateDto dto, string userId)
        {
            var oldCourse = courseRepo.FindById(id);
            if (oldCourse.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this course.");
            }
            dtoProvider.Mapper.Map(dto, oldCourse);
            courseRepo.Update(oldCourse);
        }

        public CourseViewDto GetCourse(string id)
        {
            var course = courseRepo.FindById(id);
            return dtoProvider.Mapper.Map<CourseViewDto>(course);
        }
    }
}

