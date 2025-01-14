using System;
using System.Collections.Generic;

namespace InstitudeManagement.Models;

public partial class Balance
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public decimal ReceivedAmount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual Addmission1 Student { get; set; } = null!;
}
