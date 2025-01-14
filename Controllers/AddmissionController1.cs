using InstitudeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagement.Controllers
{
    public class AddmissionController1 : Controller
    {

        public readonly TeknowellContext Tekcontext;
        public AddmissionController1(TeknowellContext Tekcontext)
        {
            this.Tekcontext = Tekcontext;
        }

        public async Task<IActionResult> AddmissionTable(string searchString, DateTime? fromDate, DateTime? toDate, string sortBy = "Id")  // Addmission Table 
        {
            var entities = from e in Tekcontext.Addmission1s
                           select e;

            // Search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                entities = entities.Where(e => e.Name.Contains(searchString) ||
                                                 e.ContactNo.Contains(searchString) ||
                                                 e.CurrentAddress.Contains(searchString) ||
                                                 e.City.Contains(searchString) ||
                                                 e.Dob.Contains(searchString) ||
                                                 e.Qualification.Contains(searchString) ||
                                                 e.Email.Contains(searchString) ||
                                                 e.Course.Contains(searchString) ||
                                                 e.Gender.Contains(searchString));
            }

            // Date range filter
            if (fromDate.HasValue && toDate.HasValue)
            {
                entities = entities.Where(e => e.AddmissionDate >= fromDate.Value && e.AddmissionDate <= toDate.Value);
            }
            //CourseNavigation
            entities = entities.Include(a => a.CourseNavigation);

            // Sorting logic
            switch (sortBy)
            {
                case "Name":
                    entities = entities.OrderByDescending(e => e.Name);
                    break;
                case "Id":
                    entities = entities.OrderByDescending(e => e.Id);
                    break;
                case "AddmissionDate":
                    entities = entities.OrderByDescending(e => e.AddmissionDate);
                    break;
                case "Course":
                    entities = entities.OrderByDescending(e => e.Course);
                    break;
                // Add cases for other columns as needed
                default:
                    entities = entities.OrderByDescending(e => e.Id); // Default sort by Id
                    break;
            }

            return View(await entities.ToListAsync());
        }






        [HttpGet("AddmissionController1/Add/{id}")]
        public async Task<IActionResult> Add(int id) // Convert the inquiry into admission
        {
            var data = await Tekcontext.Inquiries.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            var add = new Addmission1
            {
                Name = data.Name,
                Email = data.Email,
                Gender = data.Gender,
                ContactNo = data.Contact,
                Course = data.Course,
                CurrentAddress = data.Address,
                City = data.City,
                InquiryId = data.Id
            };

            ViewBag.Courses = new SelectList(Tekcontext.Course1s, "CourseName", "CourseName");
            return View(add);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Addmission1 A1)
        {
            if (ModelState.IsValid)
            {
                A1.Id = 0; // Ensure the primary key is reset for a new record
                await Tekcontext.Addmission1s.AddAsync(A1);

                // Delete the associated inquiry
                var inquiry = await Tekcontext.Inquiries.FindAsync(A1.InquiryId);
                if (inquiry != null)
                {
                    Tekcontext.Inquiries.Remove(inquiry);
                }

                await Tekcontext.SaveChangesAsync();
                TempData["Create"] = "Student added successfully and inquiry deleted.";
                return RedirectToAction("AddmissionTable", "AddmissionController1");
            }

            ViewBag.Courses = new SelectList(Tekcontext.Course1s, "CourseName", "CourseName");
            return View(A1);
        }

        public IActionResult Create()   //Simple code of Add information
        {
            ViewBag.Courses = new SelectList(Tekcontext.Course1s, "CourseName", "CourseName");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Addmission1 A1)

        {
            if (ModelState.IsValid)
            {
                await Tekcontext.Addmission1s.AddAsync(A1);
                await Tekcontext.SaveChangesAsync();
                TempData["Create"] = "Student Add Successfully";
                return RedirectToAction("AddmissionTable", "AddmissionController1");
            }
            ViewBag.Courses = new SelectList(Tekcontext.Course1s, "CourseName", "CourseName");
            return View(A1);
        }
       


        public async Task<IActionResult> Edit(int? id)  //Update
        {
            if (id == null || Tekcontext.Addmission1s == null)
            {
                return NotFound();
            }
            var stdData = await Tekcontext.Addmission1s.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }
            ViewBag.Courses = new SelectList(Tekcontext.Course1s, "CourseName", "CourseName");

            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Addmission1 add)
        {
            if (id != add.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                Tekcontext.Update(add);
                await Tekcontext.SaveChangesAsync();
                TempData["Edit"] = "Student Data Update Succesfully....";
                return RedirectToAction("AddmissionTable", "AddmissionController1");
            }
            ViewBag.Courses = new SelectList(Tekcontext.Course1s, "CourseName", "CourseName");
            return View(add);
        }

        public async Task<IActionResult> Details(int? id)  //Details
        {
            if (id == null || Tekcontext.Addmission1s == null)
            {
                return NotFound();
            }
            var stdData = await Tekcontext.Addmission1s.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }


        public async Task<IActionResult> Delete(int? id) //Delete
        {
            if (id == null || Tekcontext.Addmission1s == null)
            {
                return NotFound();
            }
            var stdData = await Tekcontext.Addmission1s.FirstOrDefaultAsync(x => x.Id == id);
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
            var stdData = await Tekcontext.Addmission1s.FindAsync(id);
            if (stdData != null)
            {
                Tekcontext.Addmission1s.Remove(stdData);
            }
            await Tekcontext.SaveChangesAsync();
            TempData["Delete"] = "Delete Successfully..........";
            return RedirectToAction("AddmissionTable", "AddmissionController1");

        }


        public IActionResult Index()
        {

            return View();
        }
    }
}
