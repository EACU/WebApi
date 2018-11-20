using EACA_API.Models.Account;

namespace EACA_API.Models.Institute
{
    public class StudentGroup
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }

        public string GroupId { get; set; }
        public Group Group { get; set; }
    }
}
