using BusinessObject.DTOs;
using DataAccess;
using HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsarticleController : ControllerBase
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public NewsarticleController(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        [HttpGet("public")]
        public async Task<IActionResult> GetActiveNews()
        {
            var newsEntities = await _newsArticleRepository.GetActiveNews();

            var newsDtos = newsEntities.Select(n => new NewsArticleDTO
            {
                NewsArticleId = n.NewsArticleId,
                NewsTitle = n.NewsTitle ?? "",
                Headline = n.Headline,
                NewsContent = n.NewsContent,
                NewsSource = n.NewsSource,
                CreatedDate = n.CreatedDate,
                ModifiedDate = n.ModifiedDate,
                NewsStatus = n.NewsStatus,
                CategoryId = n.CategoryId,
                CreatedById = n.CreatedById,
                UpdatedById = n.UpdatedById,

                CategoryName = n.Category?.CategoryName,
                CreatedByName = n.CreatedBy?.AccountName,
                UpdatedByName = n.UpdatedBy?.AccountName,
                TagIds = n.Tags.Select(t => t.TagId).ToList(),
                TagNames = n.Tags.Select(t => t.TagName).ToList()
            }).ToList();

            return Ok(newsDtos);
        }

        [HttpGet("history/{staffId}")]
        public async Task<IActionResult> GetNewsHistoryByStaffId(short staffId)
        {
            var articles = await _newsArticleRepository.GetNewsHistoryByStaffIdAsync(staffId);
            if (articles == null || !articles.Any())
                return NotFound("No articles found for the specified staff ID.");

            return Ok(articles);
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var articles = await _newsArticleRepository.GetArticlesByDateRange(startDate, endDate);

            var report = articles.Select(n => new NewsArticleDTO
            {
                NewsArticleId = n.NewsArticleId,
                NewsTitle = n.NewsTitle,
                Headline = n.Headline,
                NewsContent = n.NewsContent,
                NewsSource = n.NewsSource,
                CreatedDate = n.CreatedDate,
                ModifiedDate = n.ModifiedDate,
                NewsStatus = n.NewsStatus,

                CategoryId = n.CategoryId,
                CategoryName = n.Category?.CategoryName,

                CreatedById = n.CreatedById,

                UpdatedById = n.UpdatedById,

            }).ToList();

            return Ok(report);
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsArticleRepository.GetAllAsync();
            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsArticleDTO>> GetById(string id)
        {
            var article = await _newsArticleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var result = new NewsArticleDTO
            {
                NewsArticleId = article.NewsArticleId.ToString(),
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource,
                CategoryId = article.CategoryId,
                NewsStatus = article.NewsStatus,
                CreatedById = article.CreatedById,
                ModifiedDate = article.ModifiedDate,
                UpdatedById = article.UpdatedById,
                TagIds = article.Tags?.Select(t => t.TagId).ToList()
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsArticleDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var article = new NewsArticle
            {
                NewsTitle = dto.NewsTitle,
                Headline = dto.Headline,
                NewsContent = dto.NewsContent,
                NewsSource = dto.NewsSource,
                NewsStatus = dto.NewsStatus,
                CategoryId = dto.CategoryId,
                CreatedById = dto.CreatedById,
                UpdatedById = dto.UpdatedById,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            };

            if (dto.TagIds != null && dto.TagIds.Any())
            {
                article.Tags = await _newsArticleRepository.GetTagsByIdsAsync(dto.TagIds);
            }

            await _newsArticleRepository.AddAsync(article);

            return CreatedAtAction(nameof(GetById), new { id = article.NewsArticleId }, null);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] NewsArticleDTO dto)
        {
            if (id != dto.NewsArticleId)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            var article = await _newsArticleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            article.NewsTitle = dto.NewsTitle;
            article.Headline = dto.Headline;
            article.NewsContent = dto.NewsContent;
            article.NewsSource = dto.NewsSource;
            article.NewsStatus = dto.NewsStatus;
            article.CategoryId = dto.CategoryId;
            article.ModifiedDate = dto.ModifiedDate ?? DateTime.Now;
            article.UpdatedById = dto.UpdatedById;

            if (dto.TagIds != null && dto.TagIds.Any())
            {
                article.Tags = await _newsArticleRepository.GetTagsByIdsAsync(dto.TagIds);
            }

            await _newsArticleRepository.UpdateAsync(article);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var article = await _newsArticleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            try
            {
                await _newsArticleRepository.DeleteAsync(article);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("Cannot delete this article. It may be referenced elsewhere.");
            }
        }


        //[HttpGet("search")]
        //public async Task<IActionResult> Search(string keyword)
        //{
        //    var results = await _newsArticleRepository.SearchAsync(keyword);
        //    return Ok(results);
        //}

        //[HttpGet("created-by/{staffId}")]
        //public async Task<IActionResult> GetByStaff(short staffId)
        //{
        //    var news = await _newsArticleRepository.GetByStaffAsync(staffId);
        //    return Ok(news);
        //}

        //[HttpGet("report")]
        //public async Task<IActionResult> Report(DateTime startDate, DateTime endDate)
        //{
        //    var report = await _newsArticleRepository.GetReportAsync(startDate, endDate);
        //    return Ok(report);
        //}
    }
}
