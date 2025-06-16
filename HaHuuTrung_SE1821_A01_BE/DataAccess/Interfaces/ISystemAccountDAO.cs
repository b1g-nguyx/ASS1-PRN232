using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ISystemAccountDAO
    {
        Task<List<SystemAccount>> GetAllAsync();
        Task<SystemAccount?> GetByIdAsync(short id);
        Task<SystemAccount?> GetByEmailAsync(string email);
        Task<List<SystemAccount>> GetByRoleAsync(int role);

        Task AddAsync(SystemAccount account);
        Task UpdateAsync(SystemAccount account);
        Task<bool> DeleteAsync(short accountId); 
        Task<bool> EmailExistsAsync(string email);
        Task<SystemAccount?> AuthenticateAsync(string email, string password);
    }
}
