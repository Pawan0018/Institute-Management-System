using InstitudeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagement.Controllers
{
    public class CourseController : Controller
    {

        public readonly TeknowellContext context;
        public CourseController(TeknowellContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> CourseTable()
        {
            var data = await context.Course1s
                                    .OrderByDescending(c => c.Id)  // Sort by Id in descending order
                                    .ToArrayAsync();
            return View(data);
        }



        public IActionResult Create()  // Performing CRUD Opration  .Create
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string courseName, string[] frontEnd, string[] backEnd, string[] dataBaseLanguage, string duration, int fees, string courseFormat, string courseDescription)
        {
            if (string.IsNullOrWhiteSpace(courseName) || fees <= 0 || string.IsNullOrWhiteSpace(duration) || string.IsNullOrWhiteSpace(courseFormat))
            {
                ModelState.AddModelError("", "Name, Duration, Course Fees, and Course Format are required.");
                return View();
            }

            // Save data to the database
            var course = new Course1
            {
                CourseName = courseName,
                FrontEnd = string.Join(" , ", frontEnd), // Join selected FrontEnd languages
                BackEnd = string.Join(", ", backEnd),  // Join selected BackEnd languages
                DataBaseLanguage = string.Join(" , ", dataBaseLanguage), // Join selected Database languages
                Duration = duration,
                Fees = fees,
                CourseFormate = courseFormat,
                CourseDescription = courseDescription
            };

            await context.Course1s.AddAsync(course);
            await context.SaveChangesAsync();

            TempData["Create"] = "Course created successfully!";
            return RedirectToAction("CourseTable", "Course");
        }


        public async Task<IActionResult> Edit(int? id)   //update
        {
            if (id == null || context.Course1s == null)
            {
                return NotFound();
            }
            var stdData = await context.Course1s.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }


            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Course1 std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Update(std);
                await context.SaveChangesAsync();
                TempData["Update"] = "Data Update Succesfully....";
                return RedirectToAction("CourseTable", "Course");
            }

            return View(std);
        }

        public async Task<IActionResult> Delete(int? id)  //delete
        {
            if (id == null || context.Course1s == null)
            {
                return NotFound();
            }
            var stdData = await context.Course1s.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Protect Application Attckers and Hackker
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            var stdData = await context.Course1s.FindAsync(id);
            if (stdData != null)
            {
                context.Course1s.Remove(stdData);
            }
            await context.SaveChangesAsync();
            TempData["Delete"] = "Delete Successfully..........";
            return RedirectToAction("CourseTable", "Course");

        }


        public async Task<IActionResult> Details(int? id)  //Details
        {
            if (id == null || context.Course1s == null)
            {
                return NotFound();
            }
            var stdData = await context.Course1s.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }

    }


}

