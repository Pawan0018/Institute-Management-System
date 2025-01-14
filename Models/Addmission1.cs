using System;
using System.Collections.Generic;

namespace InstitudeManagement.Models;

public partial class Addmission1
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Dob { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ParentName { get; set; } = null!;

    public string ParentContactNo { get; set; } = null!;

    public string PermanentAddress { get; set; } = null!;

    public string CurrentAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Qualification { get; set; } = null!;

    public string PreviousSchoolClg { get; set; } = null!;

    public string Marks { get; set; } = null!;

    public string Course { get; set; } = null!;

    public DateTime? AddmissionDate { get; set; }

    public string RegistrationFees { get; set; } = null!;

    public string PaymentMode { get; set; } = null!;

    public string? TransactionId { get; set; }

    public int CourseFees { get; set; }

    public int Installment { get; set; }

    public string? FirstDate { get; set; }

    public int? FristInsta { get; set; }

    public string? SecondDate { get; set; }

    public int? SecondInsta { get; set; }

    public string? ThirdDate { get; set; }

    public int? ThirdInsta { get; set; }

    public string? FourthDate { get; set; }

    public int? FourthInsta { get; set; }

    public string? FifthDate { get; set; }

    public int? FifthInsta { get; set; }

    public string? SixDate { get; set; }

    public int? SixInsta { get; set; }

    public string? AdmissionTakenBy { get; set; }

    public int? InquiryId { get; set; }

    public string? Dicount { get; set; }

    public decimal? DicountedCourceFees { get; set; }

    public string? Remark { get; set; }

    public int? CourseId { get; set; }

    public string? FrontEnd { get; set; }

    public string? BackEnd { get; set; }

    public string? DataBaseLanguage { get; set; }

    public string? AadharcardNo { get; set; }

    public virtual ICollection<Balance> Balances { get; set; } = new List<Balance>();

    public virtual Course1? CourseNavigation { get; set; }

    public virtual Inquiry? Inquiry { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
