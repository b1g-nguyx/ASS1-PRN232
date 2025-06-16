using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class GenerateReportController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiBase;

        public GenerateReportController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _apiBase = configuration["ApiSettings:AuthBaseUrl"];
        }

        [HttpGet]
        public IActionResult GenerateReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var client = _clientFactory.CreateClient();
            var url = $"{_apiBase}/report?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

            var response = await client.GetAsync(url);

            List<NewsArticleDTO> reportData = new();

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                reportData = JsonConvert.DeserializeObject<List<NewsArticleDTO>>(jsonData);
            }
            else
            {
                ModelState.AddModelError("", "Failed to load report data.");
            }

            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
            return View(reportData);
        }
    }
}
