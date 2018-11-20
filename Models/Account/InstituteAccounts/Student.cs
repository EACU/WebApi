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
        public string ApiUserId { get; set; }
        public ApiUser ApiUser { get; set; }

        public ICollection<StudentGroup> StudentGroups { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
