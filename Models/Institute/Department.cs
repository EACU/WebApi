﻿using System.ComponentModel.DataAnnotations;

namespace EACA_API.Models.Institute
{
    public class Department
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
