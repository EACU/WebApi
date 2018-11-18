using System.Collections.Generic;

namespace EACA_API.Models.Institute
{
    public class Subject
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Credits { get; set; }
        public int HoursInSemester { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<SubjectAssignment> SubjectAssignment { get; set; }
    }
}
