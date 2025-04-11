using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsDetails.Context;
using StudentsDetails.Models;
using StudentsDetails.Repository;
using StudentsDetails.Repository.IRepository;

namespace StudentsDetails.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //ViewBag.courseName = new SelectList(_context.courses.ToList(), "course", "course");

            var courseList = await _studentRepository.DisplayallCourse();

            var model = new CourseViewModel
            {
                 availableCourse = courseList
            };

          
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                courseViewModel.student.RegisteredDate = DateTime.Now;

                await _studentRepository.CreateAsync(courseViewModel.student);

                TempData["SuccessMessage"] = $"Student {courseViewModel.student.Name} added successfully";

                var courseList = await _studentRepository.DisplayallCourse();

                var model = new CourseViewModel
                {
                    availableCourse = courseList
                };


                return View(model);

               
            }


            return View();
        }

        
        [Authorize(Roles = "SUPERADMIN,ADMIN,DEAN,STAFF")]
        [HttpGet]
        public async Task<IActionResult> ViewRegistration()
        {
            var result = await _studentRepository.GetAllAsync();

            if (result != null)
            {
                return View(result);
            }

            return View();
            
        }


        [Authorize(Roles = "SUPERADMIN,ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentRepository.GetByIdAsync(x => x.Id == id);

            if (result != null)
            {
                return View(result);

            }

            return View();
            
        }

        [Authorize(Roles = "SUPERADMIN,ADMIN")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConform(int id)
        {
            var result = await _studentRepository.GetByIdAsync(x =>x.Id == id);

            if (result != null)
            {
                await _studentRepository.DeleteAsync(result);

                return RedirectToAction("ViewRegistration");
            }

            return View();
        }






    }
}
