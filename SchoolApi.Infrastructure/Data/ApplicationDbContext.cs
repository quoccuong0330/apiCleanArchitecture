using System.Drawing;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<UserEntity?> User { get; set; }
        public DbSet<ClassEntity> Class { get; set; }
        public DbSet<PointEntity> Point { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(u => u.ClassId)
                .OnDelete(DeleteBehavior.SetNull); // Cho phép SET NULL khi Class bị xóa
            // Quan hệ Class -> Lead (User)
            modelBuilder.Entity<ClassEntity>()
                .HasOne(c => c.Lead)
                .WithMany()
                .HasForeignKey(c => c.LeadId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.TablePoint)
                .WithOne(tp => tp.Student)
                .HasForeignKey<PointEntity>(tp => tp.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<PointEntity>()
                .HasOne(t => t.Editor)
                .WithMany()
                .HasForeignKey(t => t.EditorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}