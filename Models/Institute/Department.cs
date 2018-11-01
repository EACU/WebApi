using System.Collections.Generic;

namespace EACA_API.Models.Institute
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Budget { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
