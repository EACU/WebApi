using EACA_API.Models.Account;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EACA_API.Models.Institute
{
    public class Group
    {
        public string Id { get; set; }

        [Required]
        public int Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string HeadmanId { get; set; }
        public ApiUser Headman { get; set; }

        [Required]
        public string CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
