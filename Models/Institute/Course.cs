using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EACA_API.Models.Institute
{
    public class Course
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PeriodOfStudy { get; set; }

        [Required]
        public bool Budget { get; set; }

        public long Cost { get; set; }

        [Required]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
