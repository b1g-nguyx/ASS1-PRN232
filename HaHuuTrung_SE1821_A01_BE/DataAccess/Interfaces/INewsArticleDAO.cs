using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface INewsArticleDAO
    {
        Task<List<NewsArticle>> GetAllAsync();
        Task<NewsArticle?> GetByIdAsync(string id);
        Task AddAsync(NewsArticle article);
        Task UpdateAsync(NewsArticle article);
        Task DeleteAsync(NewsArticle article);

        Task<List<NewsArticle>> GetActiveNews();
        Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds);
        Task<List<NewsArticle>> GetNewsHistoryByStaffIdAsync(short staffId);
        Task<List<NewsArticle>> GetArticlesByDateRange(DateTime startDate, DateTime endDate);
        Task<bool> HasArticlesByAccountId(short accountId);
    }
}
