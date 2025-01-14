using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagement.Models;

public partial class TeknowellContext : DbContext
{
    public TeknowellContext()
    {
    }

    public TeknowellContext(DbContextOptions<TeknowellContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addmission1> Addmission1s { get; set; }

    public virtual DbSet<Balance> Balances { get; set; }

    public virtual DbSet<Course1> Course1s { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Inquiry> Inquiries { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DESKTOP-Q6PNCCE\\MSSQLSERVER1;Database=Teknowell;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addmission1>(entity =>
        {
            entity.ToTable("Addmission1");

            entity.Property(e => e.AadharcardNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AddmissionDate).HasColumnType("datetime");
            entity.Property(e => e.AdmissionTakenBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BackEnd).IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Course)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CurrentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DataBaseLanguage).IsUnicode(false);
            entity.Property(e => e.Dicount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DicountedCourceFees).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FifthDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FourthDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FrontEnd).IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InquiryId).HasColumnName("Inquiry_id");
            entity.Property(e => e.Marks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ParentContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ParentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PermanentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PreviousSchoolClg)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationFees)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark).IsUnicode(false);
            entity.Property(e => e.SecondDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SixDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ThirdDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CourseNavigation).WithMany(p => p.Addmission1s)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("CourseId");

            entity.HasOne(d => d.Inquiry).WithMany(p => p.Addmission1s)
                .HasForeignKey(d => d.InquiryId)
                .HasConstraintName("addInquiry");
        });

        modelBuilder.Entity<Balance>(entity =>
        {
            entity.ToTable("Balance");

            entity.Property(e => e.ReceivedAmount).HasColumnType("decimal(10, 4)");

            entity.HasOne(d => d.Student).WithMany(p => p.Balances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student");
        });

        modelBuilder.Entity<Course1>(entity =>
        {
            entity.ToTable("Course1");

            entity.Property(e => e.BackEnd)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourseDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourseFormate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DataBaseLanguage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FrontEnd)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CurrentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfJoing)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdproofNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IDProofNumber");
            entity.Property(e => e.IdproofType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IDProofType");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PermanentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PreviesExperiance)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpecializedSubject)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inquiry>(entity =>
        {
            entity.ToTable("Inquiry");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Course)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InquiryDate).HasColumnType("datetime");
            entity.Property(e => e.InquiryTakonBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CourseNavigation).WithMany(p => p.Inquiries)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("CourseCId");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Pid);

            entity.ToTable("Payment");

            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FeesStatus).HasColumnType("decimal(10, 4)");
            entity.Property(e => e.Gmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Narration).IsUnicode(false);
            entity.Property(e => e.ParentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReceiptNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReceiptRemark).IsUnicode(false);
            entity.Property(e => e.ReceivedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReceivedMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TermCondition)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("StudentId");
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.ToTable("Register");

            entity.Property(e => e.ConfirmPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
