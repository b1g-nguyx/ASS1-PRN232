using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaHuuTrung_SE1821_A01_DataAccess.DAO
{
    public class SystemAccountDAO : ISystemAccountDAO
    {
        private readonly FUNewsDbContext _context;

        public SystemAccountDAO(FUNewsDbContext context)
        {
            _context = context;
        }

        public async Task<List<SystemAccount>> GetAllAsync()
        {
            return await _context.SystemAccounts
                                 .Include(a => a.NewsArticles)
                                 .ToListAsync();
        }

        public async Task<SystemAccount?> GetByIdAsync(short id)
        {
            return await _context.SystemAccounts
                                 .Include(a => a.NewsArticles)
                                 .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public async Task<SystemAccount?> GetByEmailAsync(string email)
        {
            return await _context.SystemAccounts
                                 .FirstOrDefaultAsync(a => a.AccountEmail == email);
        }

        public async Task<List<SystemAccount>> GetByRoleAsync(int role)
        {
            return await _context.SystemAccounts
                                 .Where(a => a.AccountRole == role)
                                 .ToListAsync();
        }

        public async Task AddAsync(SystemAccount account)
        {
            var maxId = await _context.SystemAccounts
                .MaxAsync(a => (short?)a.AccountId) ?? 0;

            account.AccountId = (short)(maxId + 1);

            await _context.SystemAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(SystemAccount account)
        {
            var hasArticles = await _context.NewsArticles
                                            .AnyAsync(n => n.CreatedById == account.AccountId);
            if (!hasArticles)
            {
                _context.SystemAccounts.Remove(account);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(short accountId)
        {
            var account = await _context.SystemAccounts
                                        .FirstOrDefaultAsync(a => a.AccountId == accountId);
            if (account == null)
                return false;

            return await DeleteAsync(account);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.SystemAccounts.AnyAsync(a => a.AccountEmail == email);
        }

        public async Task<SystemAccount?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.SystemAccounts
                                     .FirstOrDefaultAsync(a => a.AccountEmail == email);

            if (user != null && user.AccountPassword == password)
            {
                return user;
            }

            return null;
        }
    }
}
