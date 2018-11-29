using System.Security.Claims;
using System.Threading.Tasks;

namespace EACA_API.Services
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
    }
}
