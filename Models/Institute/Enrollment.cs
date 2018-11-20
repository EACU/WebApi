using EACA_API.Models.Account;
using System;

namespace EACA_API.Models.Institute
{
    public class Enrollment
    {
        public string Id { get; set; }
        public bool? Complete { get; set; }
        public string Grade { get; set; }
        public DateTime Date { get; set; }

        public string GroupId { get; set; }
        public Group Group { get; set; }

        public string SubjectId { get; set; }
        public Subject Subject { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
