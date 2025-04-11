using System.ComponentModel.DataAnnotations;

namespace StudentsDetails.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string UserRole { get; set; }
    }
}
