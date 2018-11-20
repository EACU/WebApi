using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EACA_API.Models.Institute
{
    public enum FormEducation
    {
        FullTime,
        PartTime
    }

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
        public FormEducation FormEducation { get; set; }

        public string PlacesInfoId { get; set; }
        public PlacesInfo PlacesInfo { get; set; }

        [Required]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Group> Groups { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
