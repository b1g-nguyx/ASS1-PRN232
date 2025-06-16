using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class ProfileDAO : IProfileDAO
    {
        private readonly FUNewsDbContext _context;

        public ProfileDAO(FUNewsDbContext context)
        {
            _context = context;
        }
       

        public async Task<SystemAccount> GetProfileStaffByIdAsync(short staffId)
        {
            return await _context.SystemAccounts.FindAsync(staffId);
        }

        public async Task UpdateProfileAsync(SystemAccount user)
        {
            _context.SystemAccounts.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
