using System.ComponentModel.DataAnnotations;

namespace EACA_API.Models.Institute
{
    public class Faculty
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
