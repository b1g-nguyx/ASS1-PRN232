using HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Threading.Tasks;
using DataAccess; 
using BusinessObject.DTOs; 
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        // GET: api/Profile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(short id)
        {
            var user = await _profileRepository.GetProfileStaffByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // PUT: api/Profile/{id}s
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(short id, [FromBody] ProfileDTO dto)
        {
            var user = await _profileRepository.GetProfileStaffByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            // Update fields
            user.AccountName = dto.FullName;

            await _profileRepository.UpdateProfileAsync(user);
            return Ok(new { message = "Profile updated successfully" });
        }
    }
}
