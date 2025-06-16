using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProfileDAO
    {
        Task<SystemAccount> GetProfileStaffByIdAsync(short staffId);
        Task UpdateProfileAsync(SystemAccount account);
    }
}
