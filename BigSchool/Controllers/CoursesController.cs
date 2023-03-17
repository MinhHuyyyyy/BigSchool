using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        public CoursesController()
        {
            _DbContext = new ApplicationDbContext();
        }
        // GET: Courses
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                categories = _DbContext.Categories.ToList(),    /*.Categories.ToList()*/
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel) {
            {
                if (!ModelState.IsValid)
                {
                    viewModel.categories = _DbContext.Categories.ToList();
                    return View("Create", viewModel);
                }
                var course = new Course()
                {
                    LecturerId = User.Identity.GetUserId(),
                    DateTime = viewModel.GetDateTime(),
                    CategoryId = viewModel.Category,
                    Place = viewModel.Place,
                };
                _DbContext.Courses.Add(course);
                _DbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        
        }
    }
}