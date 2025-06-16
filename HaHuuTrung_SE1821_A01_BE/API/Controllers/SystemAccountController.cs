using BusinessObject.DTOs;
using DataAccess;
using HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAccountController : ControllerBase
    {
        private readonly ISystemAccountRepository _systemAccountRepository;
        private readonly INewsArticleRepository _newsArticleRepository;

        public SystemAccountController(ISystemAccountRepository systemAccountRepository,
                                       INewsArticleRepository newsArticleRepository)
        {
            _systemAccountRepository = systemAccountRepository;
            _newsArticleRepository = newsArticleRepository;
        }

        // GET: api/SystemAccount
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _systemAccountRepository.GetAllAsync();
            var accountDTOs = accounts.Select(a => new SystemAccountDTO
            {
                AccountId = a.AccountId,
                AccountName = a.AccountName,
                AccountEmail = a.AccountEmail,
                AccountRole = a.AccountRole,
                AccountPassword = a.AccountPassword 
            }).ToList();

            return Ok(accountDTOs);
        }

        // GET: api/SystemAccount/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(short id)
        {
            var account = await _systemAccountRepository.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            var dto = new SystemAccountDTO
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole,
                AccountPassword = account.AccountPassword
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SystemAccountDTO dto)
        {
           
            var existing = await _systemAccountRepository.EmailExistsAsync(dto.AccountEmail);
            if (existing == false)
            {
                return BadRequest("Email already exists.");
            }

            var newAccount = new SystemAccount
            {
                AccountName = dto.AccountName,
                AccountEmail = dto.AccountEmail,
                AccountRole = dto.AccountRole,
                AccountPassword = dto.AccountPassword
            };

            await _systemAccountRepository.AddAsync(newAccount);
            return Ok("Account created successfully.");
        }


        // PUT: api/SystemAccount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(short id, [FromBody] SystemAccountDTO dto)
        {
            var existing = await _systemAccountRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.AccountName = dto.AccountName;
            existing.AccountEmail = dto.AccountEmail;
            existing.AccountRole = dto.AccountRole;
            existing.AccountPassword = dto.AccountPassword; 

            await _systemAccountRepository.UpdateAsync(existing);
            return Ok("Account updated successfully.");
        }

        // DELETE: api/SystemAccount/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var account = await _systemAccountRepository.GetByIdAsync(id);
            if (account == null)
                return NotFound("Account not found.");

            var hasArticles = await _newsArticleRepository.HasArticlesByAccountId(id);
            if (hasArticles)
                return BadRequest("Cannot delete this account because it has created articles.");

            await _systemAccountRepository.DeleteAsync(id);
            return Ok("Account deleted successfully.");
        }
    }
}
