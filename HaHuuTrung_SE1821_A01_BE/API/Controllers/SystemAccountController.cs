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
            return Ok(accounts);
        }

        // GET: api/SystemAccount/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(short id)
        {
            var account = await _systemAccountRepository.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        // POST: api/SystemAccount
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SystemAccount account)
        {
            await _systemAccountRepository.AddAsync(account);
            return Ok("Account created successfully.");
        }

        // PUT: api/SystemAccount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(short id, [FromBody] SystemAccount updated)
        {
            var existing = await _systemAccountRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.AccountName = updated.AccountName;
            existing.AccountEmail = updated.AccountEmail;
            existing.AccountRole = updated.AccountRole;

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
