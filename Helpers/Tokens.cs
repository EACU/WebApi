using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EACA_API.Data;
using EACA_API.Models;
using EACA_API.Models.AccountEntities.Tokens;
using EACA_API.Services;

namespace EACA_API.Helpers
{
    public class Tokens
    {
        public static async Task<TokenResponse> GenerateJwt(ApplicationDbContext context, ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions)
        {
            var refreshToken = await GenerateRefreshToken(context, identity.Name, jwtOptions);

            return new TokenResponse
            {
                AccessToken = await jwtFactory.GenerateEncodedToken(userName, identity),
                RefreshToken = refreshToken.Token,
                Expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };
        }

        private static async Task<RefreshToken> GenerateRefreshToken(ApplicationDbContext context, string userId, JwtIssuerOptions jwtOptions)
        {
            var isTokenInDB = await context.RefreshTokens.Include(x => x.User).SingleOrDefaultAsync(x => x.UserId == userId);

            if (isTokenInDB != null)
            {
                context.RefreshTokens.Remove(isTokenInDB);
                await context.SaveChangesAsync();
            }

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                IssuedUtc = jwtOptions.NotBefore,
                ExpiresUtc = jwtOptions.Expiration
            };

            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();

            return refreshToken;
        }
    }
}
