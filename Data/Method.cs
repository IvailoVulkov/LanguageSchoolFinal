namespace LanguageSchool.Data
{
    public class Method
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime RegData { get; set; }

        public ICollection <SchoolYear> SchoolYears { get; set; }
    }
}
