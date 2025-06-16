using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProfileRepository
    {
        Task<SystemAccount> GetProfileStaffByIdAsync(short staffId);
        Task UpdateProfileAsync(SystemAccount account);
    }
}
