using InstitudeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagement.Controllers
{
    public class UserController : Controller
    {
        public readonly TeknowellContext tek;
        private object toDate;
        private object fromDate;

        public UserController(TeknowellContext tek)
        {
            this.tek = tek;
        }

        public async Task<IActionResult> Table(string searchString, DateTime? fromDate, DateTime? toDate)
        {
            var entities = from e in tek.Inquiries
                           select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                entities = entities.Where(e => e.Name.Contains(searchString) ||
                                                e.Contact.Contains(searchString) ||
                                                e.Address.Contains(searchString) ||
                                                e.City.Contains(searchString) ||
                                                e.Dob.Contains(searchString) ||
                                                e.Qualification.Contains(searchString) ||
                                                e.Email.Contains(searchString) ||
                                                e.Course.Contains(searchString) ||
                                                e.Remark.Contains(searchString) ||
                                                e.InquiryTakonBy.Contains(searchString) ||
                                                e.Gender.Contains(searchString));
            }


            // Date range filter
            if (fromDate.HasValue && toDate.HasValue)
            {
                entities = entities.Where(e => e.InquiryDate >= fromDate.Value && e.InquiryDate <= toDate.Value);
            }
            // Order by Id in descending order
            entities = entities.OrderByDescending(e => e.Id);

            //CourseNavigation
            entities = entities.Include(a => a.CourseNavigation);

            return View(await entities.ToListAsync());
        }


        public IActionResult Create()    // CRUD opration of inquiry page/ Dashboard
        {
            ViewBag.Courses = new SelectList(tek.Course1s, "CourseName", "CourseName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Inquiry I1)

        {
            if (ModelState.IsValid)
            {
                await tek.Inquiries.AddAsync(I1);
                await tek.SaveChangesAsync();
                TempData["Created"] = "Inquiry Added Successfully";
                return RedirectToAction("Table", "User");
            }
            ViewBag.Courses = new SelectList(tek.Course1s, "CourseName", "CourseName");
            return View(I1);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || tek.Inquiries == null)
            {
                return NotFound();
            }
            var stdData = await tek.Inquiries.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }

            ViewBag.Courses = new SelectList(tek.Course1s, "CourseName", "CourseName");
            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Inquiry std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                tek.Update(std);
                await tek.SaveChangesAsync();
                TempData["Update"] = "Data Update Succesfully....";
                return RedirectToAction("Table", "User");
            }
            ViewBag.Courses = new SelectList(tek.Course1s, "CourseName", "CourseName");
            return View(std);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || tek.Inquiries == null)
            {
                return NotFound();
            }
            var stdData = await tek.Inquiries.FirstOrDefaultAsync(x => x.Id == id);
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
            var stdData = await tek.Inquiries.FindAsync(id);
            if (stdData != null)
            {
                tek.Inquiries.Remove(stdData);
            }
            await tek.SaveChangesAsync();
            TempData["Delete"] = "Delete Successfully..........";
            return RedirectToAction("Table", "User");

        }

        



    }
}
