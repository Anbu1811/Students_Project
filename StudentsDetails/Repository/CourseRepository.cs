using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsDetails.Context;
using StudentsDetails.Models;
using StudentsDetails.Repository.IRepository;

namespace StudentsDetails.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<SelectListItem>> DisplayCourseAsync()
        {
            var listofCourse = await _dbContext.courses.Select(c => new SelectListItem
            {
                Text = c.CourseName,
                Value = c.CourseName
            }).ToListAsync();

           

            return listofCourse;
        }

        public async Task<bool> GetByCourseAsync(string courseName)
        {
            var exist = await _dbContext.courses.AsNoTracking().FirstOrDefaultAsync(x => x.CourseName.ToLower().Trim() == courseName.ToLower().Trim());

            if (exist != null)
            {
                return true;
            }

            return false;
        }

        public async Task UpdateAsync(Course course)
        {
             _dbContext.courses.Update(course);
            await _dbContext.SaveChangesAsync();


        }
    }
}
