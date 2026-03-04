using BizSecureDemo22180059.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BizSecureDemo22180059.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        // Взимаме ID-то на логнатия потребител
        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Четем личните поръчки на потребителя
        var myOrders = await _db.Orders
            .Where(o => o.UserId == uid)
            .OrderByDescending(o => o.Id)
            .ToListAsync();

        // Четем абсолютно всички поръчки от базата
        var allOrders = await _db.Orders
            .OrderByDescending(o => o.Id)
            .ToListAsync();

        // Подаваме всички поръчки към изгледа чрез ViewBag
        ViewBag.AllOrders = allOrders;

        return View(myOrders);
    }
}