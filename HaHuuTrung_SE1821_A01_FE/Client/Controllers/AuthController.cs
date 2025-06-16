using Client.Models;
using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiBase;
        public AuthController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _apiBase = configuration["ApiSettings:AuthBaseUrl"];
        }

        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBase}/api/auth/login", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Incorrect password or email.";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthResponse>(json);

          
            HttpContext.Session.SetString("email", result.AccountEmail);
            HttpContext.Session.SetInt32("role", result.AccountRole);

            if (result.AccountRole == 1)
            {
                return RedirectToAction("Dashboard", "Staff");
            }
            else if (result.AccountRole == 2)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ViewBag.Error = "Unknown user role.";
                return View();
            }
        }


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBase}/api/auth/register", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Email already exists or invalid input.";
                return View();
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
