using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageSchool.Data
{
    public class SchoolYear
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Courses { get; set; }

        public string SchoolYears { get; set; } ///name 2022/2023

        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public int Duration { get; set; }

        public int MethodId { get; set; }
        public Method Methods { get; set; }


        [Column(TypeName="decimal(10,2)")]
        public double Price { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal BookPrice { get; set; }

        public DateTime RegData { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
