using Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace PathComTr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountService _accountService;
        public ITokenService _tokenService;
        public IQueueService _queueService;

        public AccountController(IAccountService accountService, ITokenService tokenService, IQueueService queueService)
        {
            _queueService = queueService;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("{Username},{Password}")]
        public IActionResult Authenticate(string Username, string Password)
        {
            try
            {
                var account = _accountService.GetAccount(Username, Password);

                if (account != null)
                {
                    string token = _tokenService.CreateToken(account);

                    return Ok(new { token, account.Username });
                }

                return Unauthorized(new { message = "Username or password is incorrect" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult queueTest(string Username, string Password)
        {
            try
            {
                _queueService.Send("test", "mesajdır bu mesaj");

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
