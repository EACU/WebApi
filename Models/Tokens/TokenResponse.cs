namespace EACA_API.Models.AccountEntities.Tokens
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public double Expires_in { get; set; }
    }
}
