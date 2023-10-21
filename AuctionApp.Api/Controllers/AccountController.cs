using AuctionApp.Business.AccountServices;
using AuctionApp.Domain.DTO.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
     
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO model)
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
    }
}
