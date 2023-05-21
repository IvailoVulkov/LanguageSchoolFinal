namespace LanguageSchool.Data
{
    public class RegCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Courses { get; set; }
       
        public string StudentId { get; set; }
        public Student Students { get; set; }

        public int SertificateNo { get; set; }
        public string SertificateImg { get; set; }

        public DateTime RegData { get; set; }
    }
}
