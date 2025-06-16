using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaHuuTrung_SE1821_A01_DataAccess.DAO
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly FUNewsDbContext _context;

        public CategoryDAO(FUNewsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                                 .Include(c => c.ParentCategory)
                                 .Include(c => c.InverseParentCategory)
                                 .Include(c => c.NewsArticles)
                                 .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(short id)
        {
            return await _context.Categories
                                 .Include(c => c.NewsArticles)
                                 .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
