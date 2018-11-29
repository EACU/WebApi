namespace EACA_API.Models.Email
{
    public class EmailOptions
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool SSL { get; set; }
    }
}
