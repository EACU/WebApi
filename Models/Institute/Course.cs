using System.Collections.Generic;

namespace EACA_API.Models.Institute
{
    public class Course
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Credits { get; set; }

        public string DepartmentId { get; set; }
        public Department Departament { get; set; }

        public ICollection<CourseAssignment> CourseAssignment { get; set; }
    }
}
