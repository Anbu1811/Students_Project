using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentsDetails.Models
{
    public class CourseViewModel
    {
        public Student student { get; set; }

        public List<SelectListItem> availableCourse { get; set; }

        public string addCourseName { get; set; }
    }
}
