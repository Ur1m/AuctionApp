using AuctionApp.Business.AccountServices;
using AuctionApp.Domain.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionApp.Api.Controllers
{
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

            var result = await _accountService.RegisterAsync(model.FirstName, model.LastName, model.Username, model.Password);

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
