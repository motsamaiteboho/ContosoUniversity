using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return View(_studentRepository.GetAllStudents());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Student student)
        {
            student.EnrollmentDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _studentRepository.AddStudent(student);
                return RedirectToAction("Index");
            }
            else
                return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_studentRepository.GetStudentById(id));
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _studentRepository.UpdateStudent(student);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(student);
        }

        public IActionResult Details(int id)
        {
            var student = _studentRepository.GetStudentWithEnrollmentDetails(id);
            if (student == null)
                return NotFound();
            else
                return View(student);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_studentRepository.GetStudentById(id));
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            if(student != null)
            {
                _studentRepository.DeleteStudent(student);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to Delete student";
                return View(student);
            }
            return View();
        }


    }
}
