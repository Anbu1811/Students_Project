using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsDetails.Context;
using StudentsDetails.Models;
using StudentsDetails.Repository.IRepository;

namespace StudentsDetails.Repository
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public AuthServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccesso)
        {
            _httpContextAccessor = httpContextAccesso;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

       

        public async Task<IdentityResult> Register(ApplicationUser applicationUser)
        {
           // applicationUser.UserName = applicationUser.Email;
            
            
                var result = await _userManager.CreateAsync(applicationUser, applicationUser.PasswordHash);


            return result;

        }



        public async Task<IdentityResult> AddRoleAsync(RegisterModel registerModel)
        {
            var result = await _userManager.AddToRoleAsync(registerModel.applicationUser, registerModel.SelectRole.ToString());

            return result;
        }

        public List<SelectListItem> GetRoles()
        {
           
            var role = _context.roles.Select(r => new SelectListItem
            {
                Text = r.UserRole,
                Value = r.UserRole

            }).ToList();

            return role;
        }

        public async Task<string> Login(ApplicationUser applicationUser)
        {
            var existUser = await _userManager.FindByNameAsync(applicationUser.UserName);

            if (existUser != null)
            {
                var _sigin = await _signInManager.PasswordSignInAsync(applicationUser.UserName,applicationUser.PasswordHash,isPersistent: true, lockoutOnFailure: false);

                if (_sigin.Succeeded)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("UserName",existUser.FirstName);
                    return "Successfull Login";
                }
                else
                {
                    return "Incorrect Password..";
                }

            }
            else
            {
                return "Incorrect Username";
            }
        }

        public async Task<string> LogOut()
        {
            await _signInManager.SignOutAsync();

            return "Logged out successfully";
        }

       
    }
}
