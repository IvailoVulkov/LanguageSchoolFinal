namespace LanguageSchool.Data
{
    public class Enrollment
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public Student Students { get; set; }

        public int SchoolYearId { get; set; }
        public SchoolYear SchoolYears { get; set; }
        
        public int Result { get; set; }

        public int SertificatId {get;set;}

        public DateTime RegData { get; set; }

    }
}
