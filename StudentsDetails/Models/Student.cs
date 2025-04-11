using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsDetails.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public DateTime DateofBirth { get; set; }

        //[ForeignKey("Course")]
        public int CourseId { get; set; }

        //public Course Course { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public DateTime RegisteredDate { get; set; }

       
    }
}
