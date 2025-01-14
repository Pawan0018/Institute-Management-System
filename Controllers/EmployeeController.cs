using InstitudeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagement.Controllers
{
    public class EmployeeController : Controller
    {

        public readonly TeknowellContext Tekcontext;
        public EmployeeController(TeknowellContext Tekcontext)
        {
            this.Tekcontext = Tekcontext;
        }

        public async Task<IActionResult> EmployeeTable(string searchString)  //Employee Table 
        {
            var entities = from e in Tekcontext.Employees
                           select e;

            // Apply search filter if searchString is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                entities = entities.Where(e => e.Name.Contains(searchString) ||
                                                e.Designation.Contains(searchString) ||
                                                e.Department.Contains(searchString) ||
                                                e.SpecializedSubject.Contains(searchString) ||
                                                e.Qualification.Contains(searchString) ||
                                                e.Email.Contains(searchString) ||
                                                e.Status.Contains(searchString) ||
                                                e.MaritalStatus.Contains(searchString) ||
                                                e.Gender.Contains(searchString));
            }

            // Order the results by Id in descending order
            var orderedEntities = await entities.OrderByDescending(e => e.Id).ToListAsync();

            return View(orderedEntities);
        }

        public IActionResult Create()  // Performing CRUD Opration  .Create
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee e1)

        {
            if (ModelState.IsValid)
            {
                await Tekcontext.Employees.AddAsync(e1);
                await Tekcontext.SaveChangesAsync();
                TempData["Create"] = "Data Create Successfully";
                return RedirectToAction("EmployeeTable", "Employee");
            }

            return View(e1);
        }


        public async Task<IActionResult> Edit(int? id)  //Update
        {
            if (id == null || Tekcontext.Employees == null)
            {
                return NotFound();
            }
            var stdData = await Tekcontext.Employees.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }


            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Employee emp)
        {
            if (id != emp.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                Tekcontext.Update(emp);
                await Tekcontext.SaveChangesAsync();
                TempData["Edit"] = "Data Update Succesfully....";
                return RedirectToAction("EmployeeTable", "Employee");
            }

            return View(emp);
        }

        public async Task<IActionResult> Details(int? id)  //Details
        {
            if (id == null || Tekcontext.Employees == null)
            {
                return NotFound();
            }
            var stdData = await Tekcontext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }


        public async Task<IActionResult> Delete(int? id) //Delete
        {
            if (id == null || Tekcontext.Employees == null)
            {
                return NotFound();
            }
            var stdData = await Tekcontext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Protect Application Attckers and Hackker
        public async Task<IActionResult> RemoveConfirm(int? id)
        {
            var stdData = await Tekcontext.Employees.FindAsync(id);
            if (stdData != null)
            {
                Tekcontext.Employees.Remove(stdData);
            }
            await Tekcontext.SaveChangesAsync();
            TempData["Delete"] = "Delete Successfully..........";
            return RedirectToAction("EmployeeTable", "Employee");

        }



        public IActionResult Index()
        {
            return View();
        }
    }
}



//public IActionResult Create()    // CRUD opration of inquiry page/ Dashboard
//{
//         return View();
//}
//[HttpPost]  
//public async Task<IActionResult> Create(string name, string[] frontEnd, string[] backEnd, string[] dataBaseLanguage, string duration, int fees, string courseFormat, string description)
//{
//    if (string.IsNullOrWhiteSpace(name) || fees <= 0 || string.IsNullOrWhiteSpace(duration) || string.IsNullOrWhiteSpace(courseFormat))
//    {
//        ModelState.AddModelError("", "Name, Duration, Course Fees, and Course Format are required.");
//        return View();
//    }

//    // Save data to the database
//    var courseForm = new Course
//    {
//        CourseName = name,
//        FrontEnd = string.Join(",", frontEnd.Where(x => x != "false")), // Convert array to comma-separated string
//        BackEnd = string.Join(",", backEnd.Where(x => x != "false")), // Convert array to comma-separated string
//        DataBaseLanguage = string.Join(",", dataBaseLanguage.Where(x => x != "false")), // Convert array to comma-separated string
//        Duration = duration,
//        Fees =fees,
//        CourseFormate = courseFormat,
//        CourseDescription = description

//    };

//    await context.Courses.AddAsync(courseForm);
//    await context.SaveChangesAsync();

//    return RedirectToAction("CourseTable", "Course");
//}

