using EACA_API.Models.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace EACA_API.Models.Institute
{
    [Table("StudentGroups")]
    public class StudentGroup
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }

        public string GroupId { get; set; }
        public Group Group { get; set; }

        public string Gradebook { get; set; }
    }
}
