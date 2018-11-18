using EACA_API.Models.Account;

namespace EACA_API.Models.Institute
{
    public class SubjectAssignment
    {
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }

        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
