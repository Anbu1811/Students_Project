using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsDetails.Context;
using StudentsDetails.Models;
using StudentsDetails.Repository.IRepository;

namespace StudentsDetails.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        
        [Authorize(Roles = "SUPERADMIN,ADMIN,STAFF")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       
        [Authorize(Roles = "SUPERADMIN,ADMIN,STAFF")]
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {

            if (ModelState.IsValid)
            {
                
                var exist = await _courseRepository.GetByCourseAsync(course.CourseName);
                if (exist != true)
                {

                        await _courseRepository.CreateAsync(course);

                    //TempData["Create"] = $"{course.CourseName} Course added successfully...";
                    TempData["SuccessMessage"] = $" {course.CourseName} course added successfully!";
                    return RedirectToAction("GetallCourse");

                }
                else
                {
                    TempData["Create"] = "This course already available";
                }

                

            }
            else
            {
                TempData["Create"] = "Course create Failed...";
            }

            return View();
        }


        
        
        [HttpGet]
        [Authorize(Roles = "SUPERADMIN,ADMIN,DEAN,STAFF")]
        public async Task<IActionResult> GetallCourse()
        {
            var allCourse = await _courseRepository.GetAllAsync();

            if (allCourse != null)
            {
                return View(allCourse);
            }


            return View();
        }

        
        [Authorize(Roles = "SUPERADMIN,ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var exist = await _courseRepository.GetByIdAsync(x => x.Id == id);

            if (exist != null)
            {
                return View(exist);
            }

            return View();
        }

        
        [Authorize(Roles = "SUPERADMIN,ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Update(Course updateCourse)
        {

            if (ModelState.IsValid)
            {
                await _courseRepository.UpdateAsync(updateCourse);

                return RedirectToAction("GetallCourse");
            }
            
            return View();
        }

        
        [Authorize(Roles = "SUPERADMIN,ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _courseRepository.GetByIdAsync(x => x.Id == id);

            if (exist != null)
            {
                return View(exist);
            }

            return View();
        }


        
        [Authorize(Roles = "SUPERADMIN,ADMIN")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConform(int id)
        {
            var exist = await _courseRepository.GetByIdAsync(x => x.Id == id);

            if (exist != null)
            {

                await _courseRepository.DeleteAsync(exist);


                return RedirectToAction("GetallCourse");
            }


            

            return View();
        }

    }
}
