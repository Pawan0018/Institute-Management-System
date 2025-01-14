using InstitudeManagement.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace InstitudeManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        private readonly TeknowellContext context;
        public PaymentController(TeknowellContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Click()
        {
            // Use async for fetching data from the database
            var admissions = await context.Addmission1s
                .Select(a => new
                {
                    Text = $"{a.Name} - {a.Id} - {a.ContactNo} - {a.ParentName}",
                    Value = a.Id
                })
                .ToListAsync(); // Use async method to fetch the data

            // Assign the result to ViewBag
            ViewBag.Admissions = admissions;

            return View();
        }

        public async Task<IActionResult> Payment()
        {
            var payments = await context.Payments
                                         .OrderByDescending(p => p.Pid) // Sorting by Pid in descending order
                                         .ToListAsync();

            return View(payments);
        }



        public async Task<IActionResult> Make(int id) // Use async for the method
        {
            // Fetch student data asynchronously
            var student = await context.Addmission1s
                .Where(a => a.Id == id)
                .Select(a => new
                {
                    Sid = a.Id,
                    StudentName = a.Name,
                    MobileNo = a.ContactNo,
                    Parent = a.ParentName,
                    Gmail = a.Email,
                    CourseName = a.Course,
                    Fees = a.DicountedCourceFees,
                    TotalReceivedAmount = context.Payments
            .Where(p => p.StudentId == a.Id)
            .Sum(p => (decimal?)p.ReceivedAmount) ?? 0 // Get total received amount
                })
                .FirstOrDefaultAsync(); // Use async method

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            var model = new Payment
            {
                StudentId = student.Sid,
                StudentName = student.StudentName,
                MobileNo = student.MobileNo,
                ParentName = student.Parent,
                Gmail = student.Gmail,
                CourseName = student.CourseName,
                FeesStatus = student.Fees - student.TotalReceivedAmount, // Calculate remaining fees
                ReceiptNo = GenerateReceiptNumber()
            };

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Make(Payment pay)
        {
            if (ModelState.IsValid)
            {
                // Fetch total received amount for the student so far
                var totalReceivedAmount = context.Payments
                                            .Where(p => p.StudentId == pay.StudentId)
                                            .Sum(p => (decimal?)p.ReceivedAmount) ?? 0;

                // Calculate the remaining fees
                var remainingFees = pay.FeesStatus - pay.ReceivedAmount;

                if (remainingFees < 0)
                {
                    ModelState.AddModelError("", "Received amount exceeds the outstanding fees.");
                    return View(pay);
                }
                var payment = new Payment
                {
                    StudentId = pay.StudentId,
                    StudentName = pay.StudentName,
                    MobileNo = pay.MobileNo,
                    ParentName = pay.ParentName,
                    Gmail = pay.Gmail,
                    CourseName = pay.CourseName,
                    FeesStatus = pay.FeesStatus,
                    ReceiptDate = pay.ReceiptDate == default ? DateOnly.FromDateTime(DateTime.Now) : pay.ReceiptDate,
                    Narration = pay.Narration ?? "Default Narration",
                    ReceiptNo = pay.ReceiptNo ?? GenerateReceiptNumber(),
                    ReceiptRemark = pay.ReceiptRemark ?? "No Remark",
                    ReceivedBy = pay.ReceivedBy ?? "Admin",
                    ReceivedMode = pay.ReceivedMode ?? "Cash",
                    TermCondition = pay.TermCondition ?? "Agree",
                    TransactionNumber = pay.TransactionNumber,
                    TransactionStatus = pay.TransactionStatus ?? "Pending",
                    ReceivedAmount = pay.ReceivedAmount > 0 ? pay.ReceivedAmount : 0,  // Ensure ReceivedAmount is not negative
                    TransactionDate = pay.TransactionDate ?? DateOnly.FromDateTime(DateTime.Now)  // Default to current date if TransactionDate is null
                };

                context.Payments.Add(payment); // Add the new payment to the context
                await context.SaveChangesAsync(); // Use async SaveChanges

                return RedirectToAction("Payment", "Payment"); // Redirect to the Payment action
            }

            return View(pay); // Return the view with the payment data in case of invalid model state
        }

        private string GenerateReceiptNumber()
        {
            // Get the current year and next year to represent the academic year (e.g., 2023-2024)
            var currentYear = DateTime.Now.Year;
            var nextYear = currentYear + 1;
            var academicYear = $"{currentYear.ToString().Substring(2, 2)}-{nextYear.ToString().Substring(2, 2)}"; // Example: "23-24"

            // Generate the sequential number (this could be based on the latest receipt number in the database)
            var receiptCount = context.Payments.Count(); // Assuming you're getting this from your database
            var sequentialNumber = receiptCount + 1; // Increment based on the existing records

            // Format the receipt number like "TEPL/REC/23-24/3788"
            return $"TEPL/REC/{academicYear}/{sequentialNumber}";
        }







        public async Task<IActionResult> Edit(int? id)  //Update     View Are Pending
        {
            if (id == null || context.Payments == null)
            {
                return NotFound();
            }
            var stdData = await context.Payments.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }


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
                context.Update(add);
                await context.SaveChangesAsync();
                TempData["Edit"] = "Reccord Update Succesfully....";
                return RedirectToAction("Payment", "Payment");
            }

            return View(add);
        }

        public async Task<IActionResult> Details(int? id)  //Details
        {
            if (id == null || context.Payments == null)
            {
                return NotFound();
            }
            var stdData = await context.Payments.FirstOrDefaultAsync(x => x.Pid == id);
            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }



        public async Task<IActionResult> Delete(int? id) //Delete     View Are Pending
        {
            if (id == null || context.Payments == null)
            {
                return NotFound();
            }
            var stdData = await context.Payments.FirstOrDefaultAsync(x => x.Pid == id);
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
            var stdData = await context.Payments.FindAsync(id);
            if (stdData != null)
            {
                context.Payments.Remove(stdData);
            }
            await context.SaveChangesAsync();
            TempData["Delete"] = "Record deleted successfully!";
            return RedirectToAction("Payment", "Payment");

        }
        public async Task<IActionResult> DownloadReceipt()
        {
            // Fetch the payments data
            var payments = await context.Payments
                .OrderByDescending(p => p.Pid)
                .ToListAsync();

            // Create a CSV file
            var csv = "Receipt No,Student Name,Course,Received Amount,Transaction Date\n";

            foreach (var payment in payments)
            {
                csv += $"{payment.ReceiptNo},{payment.StudentName},{payment.CourseName},{payment.ReceivedAmount},{payment.TransactionDate}\n";
            }

            // Convert the CSV string to a byte array
            var csvBytes = Encoding.UTF8.GetBytes(csv);

            // Return the CSV file as a downloadable file
            return File(csvBytes, "text/csv", "payments.csv");
        }




    }
}