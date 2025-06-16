using DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(short id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
