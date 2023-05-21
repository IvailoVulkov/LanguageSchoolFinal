namespace LanguageSchool.Data
{
    public class AgeType
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public DateTime RegData { get; set; }

        public ICollection<Course> Courses { get;set; }
    }
}
