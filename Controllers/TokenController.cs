using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EACA_API.Data;
using EACA_API.Models;
using EACA_API.Models.AccountEntities.Tokens;
using EACA_API.Models.Entities;
using EACA_API.Services;

namespace EACA_API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public TokenController(ApplicationDbContext context, UserManager<ApiUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _context = context;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody]RefreshToken rf)
        {
            var refreshToken = await _context.RefreshTokens.Include(x => x.User).SingleOrDefaultAsync(x => x.Token == rf.Token);

            if (refreshToken == null)
                return BadRequest();

            var identity = Helpers.Claims.GenerateClaimsIdentity(refreshToken.User.UserName, refreshToken.UserId, await _userManager.GetRolesAsync(refreshToken.User));

            var jwt = await Helpers.Tokens.GenerateJwt(_context, identity, _jwtFactory, refreshToken.User.UserName, _jwtOptions);

            return Ok(jwt);
        }
    }
}