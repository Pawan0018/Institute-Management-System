using InstitudeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace InstitudeManagement.Controllers
{
    public class HomeController : Controller
    {
        public readonly TeknowellContext Tekcontext;
        public HomeController(TeknowellContext Tekcontext)
        {
            this.Tekcontext = Tekcontext;
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Login(Register R1)
        {
            var MyUser = Tekcontext.Registers.Where(x => x.Email == R1.Email && x.Password == R1.Password).FirstOrDefault();
            if (MyUser != null)
            {
                HttpContext.Session.SetString("UserSession", MyUser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed...";
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login","Home");
            }

            return View();


        }
        public IActionResult Dashboard()
        {
            var totalEnquiries =  Tekcontext.Inquiries.Count();
            var totalAdmissions =  Tekcontext.Addmission1s.Count();
            var totalEmployee = Tekcontext.Employees.Count();
            var totalCource = Tekcontext.Course1s.Count();

            // Pass the data to the view
            ViewData["TotalEnquiries"] = totalEnquiries;
            ViewData["TotalAdmissions"] = totalAdmissions;
            ViewData["totalEmployee"] = totalEmployee;
            ViewData["totalCource"] = totalCource;
 

            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.Mysession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();


        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register R2)
        {
            if (ModelState.IsValid)
            {
                await Tekcontext.AddAsync(R2);
                await Tekcontext.SaveChangesAsync();
                TempData["Success"] = "Register Successfully";
                return RedirectToAction("Login");
            }
            return View(R2);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}






