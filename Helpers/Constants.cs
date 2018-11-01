namespace EACA_API.Helpers
{
    public class Constants
    {
        public static class Jwt
        {
            public static class JwtClaimIdentifiers
            {
                public const string Role_api = "role_api";
                public const string Id = "id";
            }

            public static class JwtRoles
            {
                public const string ApiAccessAdmin = "api_access_admin";
                public const string ApiAccessInstructor = "api_access_instructor";
                public const string ApiAccessStudent = "api_access_student";
            }
        }
    }
}
