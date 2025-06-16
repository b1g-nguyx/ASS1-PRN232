
using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Controllers
{
    public class NewsarticleController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiBase;

        public NewsarticleController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _apiBase = configuration["ApiSettings:AuthBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();

            var articleRes = await client.GetAsync($"{_apiBase}/api/NewsArticle/public");
            var categoryRes = await client.GetAsync($"{_apiBase}/api/Category");
            var tagRes = await client.GetAsync($"{_apiBase}/api/Tag");

            var articles = new List<NewsArticleDTO>();
            var categories = new List<CategoryDTO>();
            var tags = new List<TagDTO>();

            if (articleRes.IsSuccessStatusCode)
            {
                var json = await articleRes.Content.ReadAsStringAsync();
                articles = JsonConvert.DeserializeObject<List<NewsArticleDTO>>(json);
            }

            if (categoryRes.IsSuccessStatusCode)
            {
                var json = await categoryRes.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(json);
            }

            if (tagRes.IsSuccessStatusCode)
            {
                var json = await tagRes.Content.ReadAsStringAsync();
                tags = JsonConvert.DeserializeObject<List<TagDTO>>(json);
            }

            ViewBag.Categories = categories;
            ViewBag.Tags = tags;

            return View(articles);
        }



        public async Task<IActionResult> Details(string id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBase}/api/Newsarticle/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var article = JsonConvert.DeserializeObject<NewsArticleDTO>(content);
                return View(article);
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsArticleDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBase}/api/Newsarticle", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create article.");
            return View(dto);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBase}/api/Newsarticle/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var article = JsonConvert.DeserializeObject<NewsArticleDTO>(content);

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, NewsArticleDTO dto)
        {
            if (id != dto.NewsArticleId)
                return BadRequest();

            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_apiBase}/api/Newsarticle/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Update failed.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBase}/api/Newsarticle/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var article = JsonConvert.DeserializeObject<NewsArticleDTO>(content);

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiBase}/api/Newsarticle/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Delete failed.";
            return RedirectToAction("Delete", new { id });
        }

        public async Task<IActionResult> HistoryByStaff(short staffId)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBase}/api/Newsarticle/history/{staffId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Cannot fetch history for this staff.";
                return View(new List<NewsArticleDTO>());
            }

            var content = await response.Content.ReadAsStringAsync();
            var articles = JsonConvert.DeserializeObject<List<NewsArticleDTO>>(content);

            return View(articles);
        }


    }
}
