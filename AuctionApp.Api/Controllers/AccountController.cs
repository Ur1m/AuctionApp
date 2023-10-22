using AuctionApp.Business.AccountServices;
using AuctionApp.Domain.DTO.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AuctionApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data provided.");
                }

                // Use the SignInManager to authenticate the user
                var result = await _accountService.LoginAsync(model);

                if (result.Succeeded)
                {
                    return Ok("Login successful.");
                }
                if (result.IsLockedOut)
                {
                    return BadRequest("Account is locked out.");
                }
                else
                {
                    return BadRequest("Invalid login attempt.");
                }
            }
            catch (System.Exception EX)
            {
                _logger.LogError(EX, "An error occurred while Login.");
                throw EX;
            }
          
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data provided.");
                }

                var result = await _accountService.RegisterAsync(model);

                if (result.Succeeded)
                {
                    return Ok("Registration was successful.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (System.Exception EX)
            {
                _logger.LogError(EX, "An error occurred while Register.");
                throw EX;
            }
         
        }
    }
}
