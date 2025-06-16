using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class TagDAO : ITagDAO
    {
        private readonly FUNewsDbContext _context;

        public TagDAO(FUNewsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAllTagAsync()
        {
            return await _context.Tags
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Tag> GetTagByIdAsync(short id)
        {
            return await _context.Tags
               .AsNoTracking()
               .FirstOrDefaultAsync(t => t.TagId == id);
        }
    }
}
