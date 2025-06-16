using DataAccess;
using DataAccess.Interfaces;
using HaHuuTrung_SE1821_A01_DataAccess.DAO;
using HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaHuuTrung_SE1821_A01_DataAccess.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly INewsArticleDAO _newsArticleDAO;

        public NewsArticleRepository(INewsArticleDAO newsArticleDAO)
        {
            _newsArticleDAO = newsArticleDAO;
        }

        public Task<List<NewsArticle>> GetAllAsync() => _newsArticleDAO.GetAllAsync();
        public Task<NewsArticle?> GetByIdAsync(string id) => _newsArticleDAO.GetByIdAsync(id);
        public Task AddAsync(NewsArticle article) => _newsArticleDAO.AddAsync(article);
        public Task UpdateAsync(NewsArticle article) => _newsArticleDAO.UpdateAsync(article);
        public Task DeleteAsync(NewsArticle article) => _newsArticleDAO.DeleteAsync(article);

        public Task<List<NewsArticle>> GetActiveNews() => _newsArticleDAO.GetActiveNews();

        public Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds) => _newsArticleDAO.GetTagsByIdsAsync(tagIds);

        public Task<List<NewsArticle>> GetNewsHistoryByStaffIdAsync(short staffId) => _newsArticleDAO.GetNewsHistoryByStaffIdAsync(staffId);

        public Task<List<NewsArticle>> GetArticlesByDateRange(DateTime startDate, DateTime endDate) => _newsArticleDAO.GetArticlesByDateRange(startDate, endDate);

        public Task<bool> HasArticlesByAccountId(short accountId) => _newsArticleDAO.HasArticlesByAccountId(accountId);
    }
}
