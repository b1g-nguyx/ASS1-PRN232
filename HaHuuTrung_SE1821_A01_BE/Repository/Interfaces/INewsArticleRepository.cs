using DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces
{
    public interface INewsArticleRepository
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
