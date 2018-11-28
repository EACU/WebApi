using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EACA_API.Models.Institute
{
    public class Group
    {
        public string Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public string CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<StudentGroup> StudentGroups { get; set; }
    }
}
