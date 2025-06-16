using DataAccess;
using DataAccess.Interfaces;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ITagDAO _dao; 
        public TagRepository(ITagDAO dao) 
        {
            _dao = dao;
        }

        public Task<List<Tag>> GetAllTagAsync() => _dao.GetAllTagAsync();

        public Task<Tag> GetTagByIdAsync(short id) => _dao.GetTagByIdAsync(id);
    }
}
