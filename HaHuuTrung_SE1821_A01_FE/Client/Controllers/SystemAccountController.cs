using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiUrl;

        public SystemAccountController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _apiUrl = configuration["ApiSettings:AuthBaseUrl"] + "/api/SystemAccount";
        }

        // GET: /SystemAccount
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(_apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to load accounts";
                return View(new List<SystemAccountDTO>());
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<SystemAccountDTO>>(content);
            return View(data);
        }

        // GET: /SystemAccount/Create
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(SystemAccountDTO dto)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_apiUrl, content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", errorMessage); 
            return View(dto);
        }


        // GET: /SystemAccount/Edit/5
        public async Task<IActionResult> Edit(short id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Account not found";
                return RedirectToAction(nameof(Index));
            }

            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<SystemAccountDTO>(content);
            return View(dto);
        }

        // POST: /SystemAccount/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(short id, SystemAccountDTO dto)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_apiUrl}/{id}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to update account");
            return View(dto);
        }

        // GET: /SystemAccount/Delete/5
        public async Task<IActionResult> Delete(short id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Cannot delete account (maybe it has created articles)";
            }
            else
            {
                TempData["Success"] = "Account deleted successfully";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
