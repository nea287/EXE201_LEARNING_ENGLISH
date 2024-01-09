using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public partial class EXE201_LEARNING_ENGLISHContext : DbContext
    {
        public EXE201_LEARNING_ENGLISHContext()
        {
        }

        public EXE201_LEARNING_ENGLISHContext(DbContextOptions<EXE201_LEARNING_ENGLISHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Certificate> Certificates { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourses { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Vouncher> Vounchers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS01;Uid=sa;Pwd=1234;Database=EXE201_LEARNING_ENGLISH;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("Account");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AccessTime).HasColumnType("datetime");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Birthdate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.District).HasMaxLength(150);

                entity.Property(e => e.FaceId).HasColumnType("text");

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.TouchId).HasColumnType("text");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(150);
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("Certificate");

                entity.Property(e => e.CertificateName).HasMaxLength(150);

                entity.Property(e => e.Image).HasColumnType("text");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Certificates)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Certificate_Teacher");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseName).HasMaxLength(150);

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Course_Category");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Course_Teacher");

                entity.HasOne(d => d.Vouncher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.VouncherId)
                    .HasConstraintName("FK_Course_Vouncher");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Content).HasMaxLength(1500);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.SlotId)
                    .HasConstraintName("FK_Feedback_Slot");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.CheckInDate).HasColumnType("datetime");

                entity.Property(e => e.FinalAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.HasOne(d => d.QuantityNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Quantity)
                    .HasConstraintName("FK_Order_Student");

                entity.HasOne(d => d.Vouncher)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VouncherId)
                    .HasConstraintName("FK_Order_Vouncher");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.CourseId });

                entity.ToTable("OrderDetail");

                entity.Property(e => e.FinalPrice).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Course");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.StudentCourse)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.StudentCourseId)
                    .HasConstraintName("FK_Slot_StudentCourse");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.StudentName).HasMaxLength(200);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK_Student_Account");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourse");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Link).HasColumnType("text");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentCourse_Student");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Level).HasColumnType("text");

                entity.Property(e => e.TeacherName).HasMaxLength(150);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK_Teacher_Account");
            });

            modelBuilder.Entity<Vouncher>(entity =>
            {
                entity.ToTable("Vouncher");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.VouncherName).HasMaxLength(150);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Vounchers)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Vouncher_Category");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
