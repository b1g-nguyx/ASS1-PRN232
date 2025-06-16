
using BusinessObject.DTOs;
using HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllTags()
        {
            var tags = await _tagRepository.GetAllTagAsync();

            var tagDTOs = tags.Select(tag => new TagDTO
            {
                TagId = tag.TagId,
                TagName = tag.TagName,
               
            }).ToList();

            return Ok(tagDTOs);
        }
    }
}
