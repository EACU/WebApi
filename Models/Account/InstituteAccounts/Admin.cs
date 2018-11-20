namespace EACA_API.Models.Account
{
    public class Admin
    {
        public string Id { get; set; }
        public string Passport { get; set; }

        public string IdentityId { get; set; }
        public ApiUser Identity { get; set; }
    }
}
