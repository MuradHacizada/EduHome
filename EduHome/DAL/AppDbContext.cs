using EduHome.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHome.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base (options)
        {

        }
        public DbSet<Slider> Sliders { get; set; } 
        public DbSet<Service> Services { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<CoursesWe> CoursesWes { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<UpEvent> UpEvents { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<AboutTeacher> AboutTeachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        
    }
}
