using Microsoft.AspNetCore.Identity;

namespace EACA_API.Models.Account
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}
