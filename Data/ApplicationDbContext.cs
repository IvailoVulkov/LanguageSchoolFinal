using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanguageSchool.Data
{
    public class ApplicationDbContext : IdentityDbContext<Student >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
 

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<AgeType> AgeTypes { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Method> Methods { get; set; }
        

        public DbSet<SchoolYear> SchoolYears { get; set; }
    }
}