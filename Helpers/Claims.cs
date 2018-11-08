using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using EACA_API.Services;
using EACA_API.Models.Account;

namespace EACA_API.Helpers
{
    public class Claims
    {
        public static async Task<ClaimsIdentity> GetClaimsIdentity(UserManager<ApiUser> userManager, IJwtFactory jwtFactory,  string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            if (await userManager.CheckPasswordAsync(userToVerify, password))
            {
                var userRoles = await userManager.GetRolesAsync(userToVerify);
                return await Task.FromResult(GenerateClaimsIdentity(userToVerify, userRoles));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public static ClaimsIdentity GenerateClaimsIdentity(ApiUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id),
                new Claim("name", $"{user.FirstName} {user.LastName}"),
                new Claim("pic", $"{user.PictureUrl}")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
                claims.Add(new Claim("rol", role));
            }

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

    }
}
