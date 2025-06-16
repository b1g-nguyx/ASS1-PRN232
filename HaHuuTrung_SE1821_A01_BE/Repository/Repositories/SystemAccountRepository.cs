using DataAccess;
using DataAccess.Interfaces;
using Repositories.Interfaces;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly ISystemAccountDAO _dao;

        public SystemAccountRepository(ISystemAccountDAO dao)
        {
            _dao = dao;
        }

        public Task<List<SystemAccount>> GetAllAsync() => _dao.GetAllAsync();

        public Task<SystemAccount?> GetByIdAsync(short id) => _dao.GetByIdAsync(id);

        public Task<SystemAccount?> GetByEmailAsync(string email) => _dao.GetByEmailAsync(email);

        public Task<bool> EmailExistsAsync(string email) => _dao.EmailExistsAsync(email);

        public Task<SystemAccount?> AuthenticateAsync(string email, string password)
            => _dao.AuthenticateAsync(email, password);

        public Task AddAsync(SystemAccount account) => _dao.AddAsync(account);

        public Task UpdateAsync(SystemAccount account) => _dao.UpdateAsync(account);

        public Task<bool> DeleteAsync(short id) => _dao.DeleteAsync(id);
    }
}
