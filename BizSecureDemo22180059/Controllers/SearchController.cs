using BizSecureDemo22180059.Data;
using BizSecureDemo22180059.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BizSecureDemo22180059.Controllers;

[Authorize]
public class SearchController : Controller
{
    private readonly AppDbContext _db;

    public SearchController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Results(string keyword)
    {
        var sql = "SELECT * FROM Orders WHERE Title LIKE @keyword";

        var param = new Microsoft.Data.SqlClient.SqlParameter("@keyword", $"%{keyword}%");

        var results = await _db.Orders
            .FromSqlRaw(sql, param)
            .ToListAsync();

        return View(results);
    }
}