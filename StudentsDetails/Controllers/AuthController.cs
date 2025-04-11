using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsDetails.Context;
using StudentsDetails.Models;
using StudentsDetails.Repository.IRepository;

namespace StudentsDetails.Controllers
{
    public class AuthController : Controller
    {
      

        private readonly IAuthServices _authServices;
        private readonly ApplicationDbContext _context;
        
        public AuthController(IAuthServices authServices, ApplicationDbContext context)
        {
            _authServices = authServices;
           _context = context;
        }

        [Authorize(Roles = "SUPERADMIN")]
        [HttpGet]
        public IActionResult Register1()
        {
            
           

            var role = _authServices.GetRoles();

            var model = new RegisterModel
            { 
                AvailableRoles = role
            };

            
            return View(model);
        }

        
        [Authorize(Roles = "SUPERADMIN")]
        [HttpPost]
        public async Task<IActionResult> Register1(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authServices.Register(registerModel.applicationUser);

                if (result.Succeeded)
                {


                    var addRole = await _authServices.AddRoleAsync(registerModel);

                    if (addRole.Succeeded)
                    {
                        TempData["SuccessMessage"] = $"User create successfully!";
                        
                    }

                    return RedirectToAction("Login");
                }
            }

            return View();
        }


       
        [HttpGet]
        public IActionResult Login()
        {


            return View();
        }

       
        
        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _authServices.Login(applicationUser);

                TempData["SuccessMessage"] = result;

                return View();
            }

            return View();
        }

     
        [Authorize(Roles = "SUPERADMIN,ADMIN,DEAN,STAFF,STUDENT")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            string result = await _authServices.LogOut();

            TempData["SuccessMessage"] = result;

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Auth");
        }






    }
}
