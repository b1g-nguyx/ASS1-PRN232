using BusinessObject.DTOs;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISystemAccountRepository _repository;

        public AuthController(ISystemAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (await _repository.EmailExistsAsync(dto.AccountEmail))
                return BadRequest("Email already exists.");

            var account = new SystemAccount
            {
                AccountName = dto.AccountName,
                AccountEmail = dto.AccountEmail,
                AccountPassword = dto.AccountPassword, // Không hash
                AccountRole = 1 // Default to Staff
            };

            await _repository.AddAsync(account);
            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _repository.AuthenticateAsync(dto.AccountEmail, dto.AccountPassword);
            if (user == null)
                return Unauthorized("Incorrect password or email.");

            return Ok(new
            {
                user.AccountId,
                user.AccountName,
                user.AccountEmail,
                user.AccountRole
            });
        }
    }
}
