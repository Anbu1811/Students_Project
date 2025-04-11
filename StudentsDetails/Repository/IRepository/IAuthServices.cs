using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsDetails.Models;

namespace StudentsDetails.Repository.IRepository
{
    public interface IAuthServices
    {
        Task<IdentityResult> Register(ApplicationUser applicationUser);

        Task<IdentityResult> AddRoleAsync(RegisterModel registerModel);

        List<SelectListItem> GetRoles();

        Task<string> Login(ApplicationUser applicationUser);

        Task<string> LogOut();
    }
}
