using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsDetails.Models;

namespace StudentsDetails.Repository.IRepository
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task UpdateAsync(Course course);

        Task<bool> GetByCourseAsync(string courseName);

        Task<List<SelectListItem>> DisplayCourseAsync();
    }
}
