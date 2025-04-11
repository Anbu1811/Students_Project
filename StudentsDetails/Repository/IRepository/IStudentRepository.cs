using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsDetails.Models;

namespace StudentsDetails.Repository.IRepository
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task UpdateAsync(Course course);

        Task<List<SelectListItem>> DisplayallCourse();
    }
}
