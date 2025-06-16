using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllTagAsync();
        Task<Tag> GetTagByIdAsync(short id);
    }
}
