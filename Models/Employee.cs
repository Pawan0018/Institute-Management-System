using System;
using System.Collections.Generic;

namespace InstitudeManagement.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string SpecializedSubject { get; set; } = null!;

    public string? Designation { get; set; }

    public string DateOfJoing { get; set; } = null!;

    public int Salary { get; set; }

    public string IdproofType { get; set; } = null!;

    public string IdproofNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Qualification { get; set; }

    public string? CurrentAddress { get; set; }

    public string? PermanentAddress { get; set; }

    public string? MaritalStatus { get; set; }

    public string? PreviesExperiance { get; set; }

    public int? ExperianceInYear { get; set; }
}
