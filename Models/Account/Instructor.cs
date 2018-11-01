using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EACA_API.Models.Institute;

namespace EACA_API.Models.Account
{
    public class Instructor
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public string IdentityId { get; set; }
        public ApiUser Identity { get; set; }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
