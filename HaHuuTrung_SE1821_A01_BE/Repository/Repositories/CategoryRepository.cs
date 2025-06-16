using DataAccess;
using DataAccess.Interfaces;
using HaHuuTrung_SE1821_A01_DataAccess.DAO;
using HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaHuuTrung_SE1821_A01_DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryDAO _categoryDAO;

        public CategoryRepository(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public Task<List<Category>> GetAllAsync() => _categoryDAO.GetAllAsync();
        public Task<Category?> GetByIdAsync(short id) => _categoryDAO.GetByIdAsync(id);
        public Task AddAsync(Category category) => _categoryDAO.AddAsync(category);
        public Task UpdateAsync(Category category) => _categoryDAO.UpdateAsync(category);
        public Task DeleteAsync(Category category) => _categoryDAO.DeleteAsync(category);
    }
}
