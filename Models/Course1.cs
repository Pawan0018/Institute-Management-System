using System;
using System.Collections.Generic;

namespace InstitudeManagement.Models;

public partial class Course1
{
    public int Id { get; set; }

    public string CourseName { get; set; } = null!;

    public string FrontEnd { get; set; } = null!;

    public string BackEnd { get; set; } = null!;

    public string DataBaseLanguage { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public int Fees { get; set; }

    public string? CourseFormate { get; set; }

    public string? CourseDescription { get; set; }

    public virtual ICollection<Addmission1> Addmission1s { get; set; } = new List<Addmission1>();

    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
