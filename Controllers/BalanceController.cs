using InstitudeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagement.Controllers
{
    public class BalanceController : Controller
    {
        private readonly TeknowellContext _context;

        public BalanceController(TeknowellContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Balance()
        {
            // Fetch students and their payment details
            var balances = await _context.Payments
                .Include(p => p.Student) // Include related student details
                .Select(p => new Balance
                {
                    Id = p.Pid, // Balance table's primary key
                    StudentId = (int)p.StudentId,
                   
                    ReceivedAmount = p.ReceivedAmount,
                    
                    /*PaymentDate = DateOnly.FromDateTime(p.PaymentDate)*/ // Convert DateTime to DateOnly
                    Student = p.Student // Include student details
                })
                .ToListAsync();

            // Pass the data to the view
            return View(balances); // Ensure the view expects IEnumerable<Balance>
        }
    }
}
