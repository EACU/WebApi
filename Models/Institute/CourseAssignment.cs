using EACA_API.Models.Account;

namespace EACA_API.Models.Institute
{
    public class CourseAssignment
    {
        public string CourseId { get; set; }
        public string InstructorId { get; set; }

        public Course Course { get; set; } 
        public Instructor Instructor { get; set; }
    }
}
