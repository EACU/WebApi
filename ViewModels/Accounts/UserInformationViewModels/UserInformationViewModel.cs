using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EACA_API.ViewModels.Accounts
{
    public class UserInformationViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
