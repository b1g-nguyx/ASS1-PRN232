using Client.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class CategoryController : Controller
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly string _apiBase;

    public CategoryController(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _apiBase = configuration["ApiSettings:AuthBaseUrl"];
    }

    public async Task<IActionResult> Index()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync($"{_apiBase}/api/Category");

        if (!response.IsSuccessStatusCode)
            return View(new List<CategoryDTO>());

        var content = await response.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<List<CategoryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ViewBag.Categories = categories;
        return View(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDTO category)
    {
        if (!ModelState.IsValid)
        {
            return View(category); 
        }

        var client = _clientFactory.CreateClient();
        var response = await client.PostAsJsonAsync($"{_apiBase}/api/Category", category);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        var error = await response.Content.ReadAsStringAsync();
        ModelState.AddModelError(string.Empty, $"API error: {response.StatusCode}. Details: {error}");
        return View(category);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync($"{_apiBase}/api/Category/{id}");

        if (!response.IsSuccessStatusCode) return NotFound();

        var content = await response.Content.ReadAsStringAsync();
        var category = JsonSerializer.Deserialize<CategoryDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View("Index", new List<CategoryDTO> { category });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDTO category)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.PutAsJsonAsync($"{_apiBase}/api/Category/{category.CategoryId}", category);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _clientFactory.CreateClient();
        await client.DeleteAsync($"{_apiBase}/api/Category/{id}");

        return RedirectToAction("Index");
    }
}
