using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EACA_API.Models.Institute;

namespace EACA_API.Models.Account
{
    public class Student
    {
        public string Id { get; set; }

        [Required]
        public string Gradebook { get; set; }

        [Required]
        public bool? Headman { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        public string IdentityId { get; set; }
        public ApiUser Identity { get; set; }

        [Required]
        public string GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
