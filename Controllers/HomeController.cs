using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BacklogManager.Models;
using BacklogManager.Data;
using BacklogManager.DTOs;

namespace BacklogManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
       var userId = HttpContext.Session.GetInt32("UserId");
        var viewModel = new DashboardViewModel();
 
        viewModel.TotalGamesInSystem = _context.Games.Count();
        
        var gamesQuery = _context.Games.AsQueryable();
        
        if (userId != null)
        {
            var userGames = _context.UserGames.Where(ug => ug.UserId == userId).ToList();
            viewModel.MyLibraryCount = userGames.Count;
            viewModel.CompletedCount = userGames.Count(ug => ug.Status == "Ukończona");
            viewModel.PlayingCount = userGames.Count(ug => ug.Status == "W trakcie");

            var ownedGameIds = userGames.Select(ug => ug.GameId).ToList();
            if (ownedGameIds.Any())
            {
                gamesQuery = gamesQuery.Where(g => !ownedGameIds.Contains(g.Id));
            }
        }
        else
        {
            viewModel.MyLibraryCount = 0;
            viewModel.CompletedCount = 0;
            viewModel.PlayingCount = 0;
        }
        viewModel.RecommendedGames = gamesQuery
            .OrderBy(g => EF.Functions.Random())
            .Take(4)
            .Select(game => new GameCreateViewModel()
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                ReleaseYear = game.ReleaseYear,
                Genre = game.Genre,
                ImageUrl = game.ImageUrl,
                IsInLibrary = false
            }).ToList();
        
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}