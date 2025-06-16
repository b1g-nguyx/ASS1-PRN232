using DataAccess;

namespace Repositories.Interfaces
{
    public interface ISystemAccountRepository
    {
        Task<List<SystemAccount>> GetAllAsync();
        Task<SystemAccount?> GetByIdAsync(short id);
        Task<SystemAccount?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<SystemAccount?> AuthenticateAsync(string email, string password);
        Task AddAsync(SystemAccount account);
        Task UpdateAsync(SystemAccount account);
        Task<bool> DeleteAsync(short id);
    }
}
