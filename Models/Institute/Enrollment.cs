using EACA_API.Models.Account;

namespace EACA_API.Models.Institute
{
    public class Enrollment
    {
        public string Id { get; set; }
        public bool? Complete { get; set; }
        public string Grade { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
