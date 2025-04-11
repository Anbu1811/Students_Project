using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsDetails.Context;
using StudentsDetails.Models;
using StudentsDetails.Repository.IRepository;

namespace StudentsDetails.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SelectListItem>> DisplayallCourse()
        {
            var course = await  _dbContext.courses.Select(c => new SelectListItem
            {
                Text = c.CourseName,
                Value = c.CourseName
            }).ToListAsync();

            return course;
        }

        public async Task UpdateAsync(Course course)
        {
            await _dbContext.courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }
    }
}
