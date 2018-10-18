using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using EACA_API.Data;
using EACA_API.Helpers;
using EACA_API.Models.Entities;
using EACA_API.ViewModels;

using AutoMapper;

namespace EACA_API.Controllers.Account
{
    [Produces("application/json")]
    [Route("api/accounts/[controller]")]
    public partial class RegisterController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterController(UserManager<ApiUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        // POST api/accounts/accounts/
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdentity = _mapper.Map<ApiUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return BadRequest(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.Students.AddAsync(new Student { IdentityId = userIdentity.Id, Group = model.Group });
            await _appDbContext.SaveChangesAsync();

            return Ok($"Аккаунт {model.Email} успешно создан!");
        }
    }
}
