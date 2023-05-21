namespace LanguageSchool.Data
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Languages Language { get; set; }
        //M:1
        public int AgeTypeId { get; set; }
        public AgeType AgeTypes { get; set; }
        //M:1
        public int CategoryId { get; set; }
        public Category Categories { get; set; }

        public string ImageUrl { get;set; }
        public string Description { get; set; }
        public DateTime RegData { get; set; }

        //1:M
        public ICollection<RegCourse> RegCourses { get; set;}
        //1:M
        public ICollection<SchoolYear> SchoolYears { get; set;}

         
    }
}
