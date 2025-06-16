using DataAccess;
using DataAccess.DAOs;
using DataAccess.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IProfileDAO _profileDAO;

        public ProfileRepository(IProfileDAO profileDAO) 
        {
            _profileDAO = profileDAO;
        }

        public Task<SystemAccount> GetProfileStaffByIdAsync(short staffId)
            => _profileDAO.GetProfileStaffByIdAsync(staffId);

        public Task UpdateProfileAsync(SystemAccount account)
            => _profileDAO.UpdateProfileAsync(account);
    }

}
