using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaHuuTrung_SE1821_A01_DataAccess.DAO
{
    public class NewsArticleDAO : INewsArticleDAO
    {
        private readonly FUNewsDbContext _context;

        public NewsArticleDAO(FUNewsDbContext context)
        {
            _context = context;
        }

        public async Task<List<NewsArticle>> GetAllAsync()
        {
            return await _context.NewsArticles
                                 .Include(a => a.Category)
                                 .Include(a => a.CreatedBy)
                                 .Include(a => a.Tags)
                                 .ToListAsync();
        }

        public async Task<NewsArticle?> GetByIdAsync(string id)
        {
            return await _context.NewsArticles
                                 .Include(a => a.Category)
                                 .Include(a => a.CreatedBy)
                                 .Include(a => a.Tags)
                                 .FirstOrDefaultAsync(a => a.NewsArticleId == id);
        }

        public async Task AddAsync(NewsArticle article)
        {
            await _context.NewsArticles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NewsArticle article)
        {
            _context.NewsArticles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(NewsArticle article)
        {
            _context.NewsArticles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NewsArticle>> GetActiveNews()
        {
            return await _context.NewsArticles
                .Where(n => n.NewsStatus == true)
                .Include(n => n.Category)
                .Include(n => n.Tags)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .OrderByDescending(n => n.CreatedDate).ToListAsync();
        }

        public async Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds)
        {
            return await _context.Tags
                                 .Where(t => tagIds.Contains(t.TagId))
                                 .ToListAsync();
        }

        public async Task<List<NewsArticle>> GetNewsHistoryByStaffIdAsync(short staffId)
        {
            return await _context.NewsArticles
            .Where(n => n.CreatedById == staffId)
            .OrderByDescending(n => n.CreatedDate)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<List<NewsArticle>> GetArticlesByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.NewsArticles
                .Where(n => n.CreatedDate.HasValue &&
                            n.CreatedDate.Value.Date >= startDate.Date &&
                            n.CreatedDate.Value.Date <= endDate.Date)
                .OrderByDescending(n => n.CreatedDate)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .ToListAsync();
        }


        public async Task<bool> HasArticlesByAccountId(short accountId)
        {
            return await _context.NewsArticles
                        .AnyAsync(n => n.CreatedById == accountId);
        }
    }
}
