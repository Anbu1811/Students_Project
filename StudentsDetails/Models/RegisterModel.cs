using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentsDetails.Models
{
    public class RegisterModel
    {
        public ApplicationUser applicationUser { get; set; }
      
        public List<SelectListItem> AvailableRoles { get; set; }
        public string SelectRole { get; set; }
    }
}
