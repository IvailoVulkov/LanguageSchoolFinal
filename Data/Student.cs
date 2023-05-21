using Microsoft.AspNetCore.Identity;

namespace LanguageSchool.Data
{
    public class Student : IdentityUser
    {
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }        
        public DateTime RegData { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<RegCourse> RegCourses { get; set;}
    }
}
