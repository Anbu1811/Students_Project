using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentsDetails.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

     
        public string UserName { get; set; }

        public string Email { get; set; }

        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        
        public string Role   { get; set; }
    }

   

    
}
